namespace ECom.Application.DTOs.Auth;

public class LoginResponseDto
{
    public string JwtToken { get; set; }
    public UserDataDto User { get; set; }
    public string RefreshToken { get; set; }
}

public class UserDataDto
{
    public string Name { get; set; }
    public string Role { get; set; }
}