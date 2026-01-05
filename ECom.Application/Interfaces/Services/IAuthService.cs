using ECom.Application.DTOs.Auth;

namespace ECom.Application.Interfaces.Services;

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
    Task RegisterAsync(RegisterRequestDto request);   
}
