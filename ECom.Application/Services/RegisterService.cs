using ECom.Application.DTOs.Register;
using ECom.Application.Interfaces.Services;
using ECom.Domain.Interfaces.Repositories;
using ECom.Domain.Interfaces.Services;

namespace ECom.Application.Services;

public class RegisterService : IRegisterService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public RegisterService(
        IUserRepository userRepository,
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }



    public async Task RegisterAsync(RegisterRequestDto request)
    {
        var existingUser = await _userRepository.GetByUsernameAsync(request.Username);
        if (existingUser != null)
            throw new Exception("Username already exists");

        var user = new ECom.Domain.Entities.User
        {
            Name = request.Name,
            Email = request.Email,
            PhoneNo = request.PhoneNo,
            Username = request.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = ECom.Domain.Enums.UserRole.User
        };

        await _userRepository.AddAsync(user);
    }
}
