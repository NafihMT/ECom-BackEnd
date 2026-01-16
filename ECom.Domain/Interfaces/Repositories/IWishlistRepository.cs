using ECom.Domain.Entities;

namespace ECom.Domain.Interfaces.Repositories;

public interface IWishlistRepository
{
    Task<WishlistItem> AddAsync(int userId, int productId);
    Task<IEnumerable<WishlistItem>> GetByUserAsync(int userId);
    Task RemoveAsync(int wishlistItemId);
}
