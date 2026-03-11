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

        if (user == null)
        {
            return new ApiResponse<LoginResponseDto>(401, "Invalid username or password");
        }

        if (user.IsBlocked)
        {
            return new ApiResponse<LoginResponseDto>(403, "User is Blocked by admin !!! ");
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return new ApiResponse<LoginResponseDto>(401, "Invalid username or password");
        }

        var accessToken = _jwtService.GenerateToken(user);
        var refreshToken = _jwtService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await _userRepository.UpdateAsync(user);

        var responseData = new LoginResponseDto
        {
            JwtToken = accessToken,
            RefreshToken = refreshToken, 
            User = new UserDataDto
            {
                Name = user.Name,
                Role = user.Role.ToString()
            }
        };

        return new ApiResponse<LoginResponseDto>(200, "Login success", responseData);
    }

    public async Task<ApiResponse<LoginResponseDto>> RefreshTokenAsync(TokenRefreshRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.RefreshToken))
        {
            return new ApiResponse<LoginResponseDto>(401, "Refresh token is required");
        }

        var user = await _userRepository.GetByRefreshTokenAsync(request.RefreshToken);

        if (user == null)
        {
            return new ApiResponse<LoginResponseDto>(401, "Invalid refresh token");
        }
        if (user.IsBlocked)
        {
            return new ApiResponse<LoginResponseDto>(403, "User account is blocked");
        }

        if (user.RefreshTokenExpiryTime == null ||
            user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return new ApiResponse<LoginResponseDto>(401, "Refresh token expired");
        }

        var newAccessToken = _jwtService.GenerateToken(user);
        var newRefreshToken = _jwtService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await _userRepository.UpdateAsync(user);

        var response = new LoginResponseDto
        {
            JwtToken = newAccessToken,
            RefreshToken = newRefreshToken,
            User = new UserDataDto
            {
                Name = user.Name,
                Role = user.Role.ToString()
            }
        };

        return new ApiResponse<LoginResponseDto>(200, "Token refreshed successfully", response);
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

    public async Task LogoutAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
            throw new Exception("User not found");

        user.RefreshToken = null;
        user.RefreshTokenExpiryTime = null;

        await _userRepository.UpdateAsync(user);
    }

}