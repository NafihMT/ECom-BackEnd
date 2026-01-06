using ECom.Application.DTOs.Auth;
using ECom.Application.DTOs.Register;
using ECom.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECom.Api.Controllers;

[ApiController]
[Route("api/register")]
public class RegisterController : ControllerBase
{
    private readonly IRegisterService _regService;

    public RegisterController(IRegisterService regService)
    {
        _regService = regService;
    }

    


    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDto request)
    {
        await _regService.RegisterAsync(request);
        return Ok(new { status = "success", message = "User registered successfully" });
    }
}
