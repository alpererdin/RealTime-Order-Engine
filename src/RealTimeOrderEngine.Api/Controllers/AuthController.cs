using Microsoft.AspNetCore.Mvc;
using RealTimeOrderEngine.Application.Interfaces.Services;
using RealTimeOrderEngine.Shared.DTOs.Auth;
using Microsoft.AspNetCore.RateLimiting;

namespace RealTimeOrderEngine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

[HttpPost("login")]
[EnableRateLimiting("login")]
public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto loginDto)
    {
        if (string.IsNullOrWhiteSpace(loginDto.PinCode))         
            return BadRequest(new { message = "PIN code cannot be empty" });

        var response = await _authService.LoginAsync(loginDto);  

        if (response == null)
            return Unauthorized(new { message = "Invalid PIN code or inactive account" });

        return Ok(response);
    }
}