namespace ECom.Domain.Interfaces.Repositories;

public interface IWishlistRepository
{
    Task AddAsync(int userId, int productId);
    Task<IEnumerable<object>> GetByUserAsync(int userId);
    Task RemoveAsync(int wishlistItemId);
}
