using ECom.Application.DTOs.Register;

namespace ECom.Application.Interfaces.Services;

public interface IRegisterService
{
    Task RegisterAsync(RegisterRequestDto request);
}
