using Pokedex.Application.Dtos.Users;

namespace Pokedex.Application.Contracts;

public interface IUserService
{
    Task<List<UserDto>> GetAll();
    Task<UserDto?> GetById(int id);
    Task<UserDto?> Create(CreateUserDto dto);
    Task<UserDto?> Update(int id, UpdateUserDto dto);
    Task Remove(int id);
}