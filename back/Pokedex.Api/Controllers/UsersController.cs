using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Application.Contracts;
using Pokedex.Application.Dtos.Users;
using Pokedex.Application.Notifications;
using Swashbuckle.AspNetCore.Annotations;

namespace Pokedex.Api.Controllers;

public class UsersController : BaseController
{
    private readonly IUserService _userService;
    public UsersController(INotificator notificator, IUserService userService) : base(notificator)
    {
        _userService = userService;
    }
    
    [HttpGet]
    [SwaggerOperation(Summary = "Get All Users.", Tags = new[] { "Users - User" })]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAll()
    {
        return OkResponse(await _userService.GetAll());
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Get User By Id.", Tags = new[] { "Users - User" })]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        return OkResponse(await _userService.GetById(id));
    }
    
    [HttpPost]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Create User.", Tags = new[] { "Users - User" })]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromForm] CreateUserDto createUserDto)
    {
        return CreatedResponse("", await _userService.Create(createUserDto));
    }
    
    [HttpPut("{id:int}")]
    [SwaggerOperation(Summary = "Update User.", Tags = new[] { "Users - User" })]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromForm] UpdateUserDto updateUserDto)
    {
        return OkResponse(await _userService.Update(id, updateUserDto));
    }
    
    [HttpDelete("{id:int}")]
    [SwaggerOperation(Summary = "Delete User.", Tags = new[] { "Users - User" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remove(int id)
    {
        await _userService.Remove(id);
        return NoContentResponse();
    }
}