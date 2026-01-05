namespace ECom.Application.Interfaces.Services;

public interface IWishlistService
{
    Task AddToWishlistAsync(int userId, int productId);
    Task<IEnumerable<object>> GetWishlistAsync(int userId);
    Task RemoveFromWishlistAsync(int itemId);
}
