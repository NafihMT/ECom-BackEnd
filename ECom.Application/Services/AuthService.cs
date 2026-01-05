using ECom.Application.DTOs.Auth;
using ECom.Application.Interfaces.Services;
using ECom.Domain.Interfaces.Repositories;
using ECom.Domain.Interfaces.Services;

namespace ECom.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public AuthService(
        IUserRepository userRepository,
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username);

        if (user == null ||
            !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid username or password");
        }

        var token = _jwtService.GenerateToken(user);

        return new LoginResponseDto
        {
            JwtToken = token
        };
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
