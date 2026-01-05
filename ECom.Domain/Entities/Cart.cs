using ECom.Domain.Common;

namespace ECom.Domain.Entities;

public class Cart : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; }

    public ICollection<CartItem> Items { get; set; }
}
