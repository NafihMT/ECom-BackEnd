namespace ECom.Application.DTOs.User;

public class UserDto
{
    public int Id { get; set; }

    public string Name { get; set; }
    public string Username { get; set; }

    public string Email { get; set; }

    public string PhoneNo { get; set; }

    public string Role { get; set; }

    public bool IsBlocked { get; set; }
}