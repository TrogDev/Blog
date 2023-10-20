namespace Blog.Web.Controllers;

using Microsoft.AspNetCore.Mvc;

using Blog.Infrastructure.Auth.Common.Abstractions;
using Blog.Infrastructure.Auth.Common.Commands;
using Blog.Infrastructure.Auth.Common.Exceptions;

[ApiController]
[Route("/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;

    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LogIn([FromForm] LogInCommand command)
    {
        try
        {
            return Ok(await authService.LogIn(command));
        }
        catch (InvalidLogInException)
        {
            return BadRequest("Неправильный логин или пароль");
        }
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromForm] RegisterCommand command)
    {
        return Ok(await authService.Register(command));
    }

    [HttpGet]
    [Route("verify")]
    public async Task<IActionResult> Verify([FromQuery] VerifyCommand command)
    {
        try
        {
            await authService.Verify(command);
        }
        catch (InvalidVerificationCode)
        {
            return BadRequest("Код верификации не найден");
        }

        return NoContent();
    }
}
