using ECom.Application.Common;
using ECom.Application.DTOs.Auth;
using ECom.Application.DTOs.Register;

namespace ECom.Application.Interfaces.Services;

public interface IAuthService
{
    Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto request);
    Task RegisterAsync(RegisterRequestDto request);
}
