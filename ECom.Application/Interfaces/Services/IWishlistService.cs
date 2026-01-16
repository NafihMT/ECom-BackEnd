using ECom.Application.DTOs.Wishlist;

namespace ECom.Application.Interfaces.Services;

public interface IWishlistService
{
    Task<WishlistItemDto> AddToWishlistAsync(int userId, int productId);
    Task<IEnumerable<WishlistItemDto>> GetWishlistAsync(int userId);
    Task RemoveFromWishlistAsync(int itemId);
}
