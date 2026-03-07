using ECom.Application.DTOs.Auth;
using ECom.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECom.Api.Controllers;

[ApiController]
[Route("api/user")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        var response = await _authService.LoginAsync(request);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(TokenRefreshRequestDto request)
    {
        var user = await _authService.RefreshTokenAsync(request);
        return Ok(user);
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return Unauthorized();

        var response = await _authService.GetProfileAsync(int.Parse(userIdClaim.Value));
        return StatusCode(response.StatusCode, response);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
            return Unauthorized();

        var userId = int.Parse(userIdClaim.Value);

        await _authService.LogoutAsync(userId);

        return Ok("Logged out successfully");
    }
}