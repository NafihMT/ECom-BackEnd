using ECom.Domain.Common;

namespace ECom.Domain.Entities;

public class Wishlist : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; }

    public ICollection<WishlistItem> Items { get; set; }
}
