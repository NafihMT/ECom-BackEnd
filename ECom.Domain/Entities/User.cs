using ECom.Domain.Common;
using ECom.Domain.Enums;

namespace ECom.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNo { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }

    public UserRole Role { get; set; }
    public bool IsBlocked { get; set; } = false;

    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }

    public Cart Cart { get; set; }
    public Wishlist Wishlist { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();

}
