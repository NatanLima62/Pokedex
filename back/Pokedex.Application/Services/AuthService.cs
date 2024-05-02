using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Security.Jwt.Core.Interfaces;
using Pokedex.Application.Contracts;
using Pokedex.Application.Dtos.Auth;
using Pokedex.Application.Dtos.Users;
using Pokedex.Application.Notifications;
using Pokedex.Core.Authorization.AuthenticatedUser;
using Pokedex.Core.Settings;
using Pokedex.Domain.Entities;
using Pokedex.Infra.Contracts;

namespace Pokedex.Application.Services;

public class AuthService : BaseService, IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly JwtSettings _jwtSettings;
    private readonly IJwtService _jwtService;
    private readonly IEmailService _emailService;
    private readonly IAuthenticatedUser _authenticatedUser;
    public AuthService(IMapper mapper, INotificator notificator, IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IOptions<JwtSettings> jwtSettings, IJwtService jwtService, IEmailService emailService, IAuthenticatedUser authenticatedUser) : base(mapper, notificator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
        _emailService = emailService;
        _authenticatedUser = authenticatedUser;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<TokenDto?> Login(LoginDto dto)
    {
        var user = await _userRepository.GetByEmail(dto.Email);
        if (user is null)
        {
            return null;
        }

        if (_passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password) == PasswordVerificationResult.Failed)
        {
            return null;
        }
        
        return new TokenDto
        {
            Token = await GenerateToken(user)
        };
    }

    public async Task SendEmailRecoverPassword(string email)
    {
        var user = await _userRepository.GetByEmail(email);
        if (user is null)
        {
            return;
        }

        user.TokenRecoverPassword = Guid.NewGuid();
        user.TokenExpiresIn = DateTime.UtcNow.AddHours(1);
        _userRepository.Update(user);
        if (await _userRepository.UnitOfWork.Commit())
        {
            _emailService.SendEmailRecoverPassword(user);
        }
    }

    public async Task ChangePassword(ChangePasswordAuthenticatedUserDto dto)
    {
        var user = await _userRepository.GetById(_authenticatedUser.Id);
        if (user is null)
        {
            Notificator.HandleNotFoundResource();
            return;
        }

        if (_passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password) !=
            PasswordVerificationResult.Success)
        {
            Notificator.Handle("Invalid password!");
            return;
        }

        if (dto.NewPassword != dto.ConfirmNewPassword)
        {
            Notificator.Handle("The password and confirmation password do not match!");
            return;
        }

        user.Password = _passwordHasher.HashPassword(user, dto.NewPassword);
        _userRepository.Update(user);
        if (!user.Validate(out var validationResult))
        {
            Notificator.Handle(validationResult.Errors);
            return;
        }

        if (!await _userRepository.UnitOfWork.Commit())
        {
            Notificator.Handle("An error occurred while changing the password");
        }
    }

    public async Task ChangePassword(ChangePasswordUserDto dto)
    {
        var user = await _userRepository.GetByEmail(dto.Email);
        if (user is null)
        {
            Notificator.HandleNotFoundResource();
            return;
        }

        if (user.TokenRecoverPassword != dto.Token || user.TokenExpiresIn < DateTime.UtcNow)
        {
            Notificator.Handle("Invalid or expired token!");
            return;
        }

        if (dto.Password != dto.ConfirmPassword)
        {
            Notificator.Handle("The password and confirmation password do not match!");
            return;
        }

        user.Password = _passwordHasher.HashPassword(user, dto.Password);
        user.TokenRecoverPassword = null;
        user.TokenExpiresIn = null;
        _userRepository.Update(user);
        if (!user.Validate(out var validationResult))
        {
            Notificator.Handle(validationResult.Errors);
            return;
        }

        if (!await _userRepository.UnitOfWork.Commit())
        {
            Notificator.Handle("An error occurred while changing the password");
        }
    }

    private async Task<string> GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var claimsIdentity = new ClaimsIdentity();
        claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
        var key = await _jwtService.GetCurrentSigningCredentials();
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.CommonValidIn,
            Subject = claimsIdentity,
            Expires = DateTime.UtcNow.AddYears(_jwtSettings.ExpirationHours),
            SigningCredentials = key
        });
        
        return tokenHandler.WriteToken(token);
    } 
}