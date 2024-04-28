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
    public AuthService(IMapper mapper, INotificator notificator, IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IOptions<JwtSettings> jwtSettings, IJwtService jwtService) : base(mapper, notificator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
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