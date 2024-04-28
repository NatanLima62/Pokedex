using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Pokedex.Application.Contracts;
using Pokedex.Application.Dtos.Users;
using Pokedex.Application.Notifications;
using Pokedex.Domain.Entities;
using Pokedex.Infra.Contracts;

namespace Pokedex.Application.Services;

public class UserService : BaseService, IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    public UserService(IMapper mapper, INotificator notificator, IUserRepository userRepository, IPasswordHasher<User> passwordHasher) : base(mapper, notificator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<List<UserDto>> GetAll()
    {
        var users = await _userRepository.GetAll();
        return Mapper.Map<List<UserDto>>(users);
    }

    public async Task<UserDto?> GetById(int id)
    {
        var user = await _userRepository.GetById(id);
        if (user is not null) return Mapper.Map<UserDto>(user);
        Notificator.HandleNotFoundResource();
        return null;
    }

    public async Task<UserDto?> Create(CreateUserDto dto)
    {
        var user = Mapper.Map<User>(dto);
        if (!await Validate(user)) return null;
        var password = _passwordHasher.HashPassword(user, user.Password);
        user.Password = password;
        
        //todo: Adicionar a logica para salvar a foto do usuario
        
        _userRepository.Add(user);
        if (await _userRepository.UnitOfWork.Commit())
        {
            return Mapper.Map<UserDto>(user);
        }

        Notificator.Handle("An error occurred while saving the user.");
        return null;
    }

    public async Task<UserDto?> Update(int id, UpdateUserDto dto)
    {
        if (id != dto.Id)
        {
            Notificator.Handle("The informed id is different from the user id.");
            return null;
        }
        
        var user = await _userRepository.GetById(id);
        if (user is null)
        {
            Notificator.HandleNotFoundResource();
            return null;
        }
        
        Mapper.Map(dto, user);
        if (!await Validate(user)) return null;
        //todo: Adicionar a logica para salvar a foto do usuario
        
        _userRepository.Update(user);
        if (await _userRepository.UnitOfWork.Commit())
        {
            return Mapper.Map<UserDto>(user);
        }

        Notificator.Handle("An error occurred while saving the user.");
        return Mapper.Map<UserDto>(user);
    }

    public async Task Remove(int id)
    {
        var user = await _userRepository.GetById(id);
        if (user is null)
        {
            Notificator.HandleNotFoundResource();
            return;
        }

        _userRepository.Remove(user);
        await _userRepository.UnitOfWork.Commit();
    }
    
    private async Task<bool> Validate(User user)
    {
        if (!user.Validate(out var validationResult))
        {
            Notificator.Handle(validationResult.Errors);
        }

        if (await _userRepository.Any(u => u.Id != user.Id && u.Email == user.Email))
        {
            Notificator.Handle("Already exists a user with those credentials.");
        }
        
        return !Notificator.HasNotifications();
    }
}