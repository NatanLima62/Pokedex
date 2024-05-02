using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Application.Contracts;
using Pokedex.Application.Dtos.Auth;
using Pokedex.Application.Dtos.Users;
using Pokedex.Application.Notifications;
using Swashbuckle.AspNetCore.Annotations;

namespace Pokedex.Api.Controllers;

[AllowAnonymous]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : BaseController
{
    private readonly IAuthService _authService;
    public AuthController(INotificator notificator, IAuthService authService) : base(notificator)
    {
        _authService = authService;
    }
    
    [HttpPost]
    [SwaggerOperation(Summary = "User Login.", Tags = new [] { "User - Auth" })]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var token = await _authService.Login(dto);
        return token != null ? OkResponse(token) : BadRequest(new[] { "User and/or password are incorrect" });
    }
    
    [HttpPost("recover-password")]
    [SwaggerOperation(Summary = "Send email to recover user password.", Tags = new [] { "User - Auth" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RecoverPassword([FromQuery] string email)
    {
        await _authService.SendEmailRecoverPassword(email);
        return NoContentResponse();
    }
    
    [HttpPost("reset-password")]
    [Authorize]
    [SwaggerOperation(Summary = "Reset user password.", Tags = new [] { "User - Auth" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordUserDto dto)
    {
        await _authService.ChangePassword(dto);
        return NoContentResponse();
    }
    
    [HttpPost("change-password")]
    [SwaggerOperation(Summary = "Change user password.", Tags = new [] { "User - Auth" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordAuthenticatedUserDto dto)
    {
        await _authService.ChangePassword(dto);
        return OkResponse();
    }
}