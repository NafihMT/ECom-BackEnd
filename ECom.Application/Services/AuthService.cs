using ECom.Application.Common;
using ECom.Application.DTOs.Auth;
using ECom.Application.DTOs.Register;
using ECom.Application.Interfaces.Services;
using ECom.Domain.Interfaces.Repositories;
using ECom.Domain.Interfaces.Services;

namespace ECom.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public AuthService(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto request)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return new ApiResponse<LoginResponseDto>(401, "Invalid username or password");
        }

        var token = _jwtService.GenerateToken(user);

        var responseData = new LoginResponseDto
        {
            JwtToken = token,
            User = new UserDataDto
            {
                Name = user.Name,
                Role = user.Role.ToString()
            }
        };

        return new ApiResponse<LoginResponseDto>(200, "Login success", responseData);
    }

    public async Task<ApiResponse<object>> GetProfileAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null) return new ApiResponse<object>(404, "User not found");

        return new ApiResponse<object>(200, "Profile retrieved", new
        {
            name = user.Name,
            role = user.Role.ToString(),
            status = "Active"
        });
    }

    public async Task RegisterAsync(RegisterRequestDto request)
    {
        var existingUser = await _userRepository.GetByUsernameAsync(request.Username);
        if (existingUser != null) throw new Exception("Username already exists");

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