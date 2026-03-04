using ECom.Application.DTOs.Cart;

namespace ECom.Application.Interfaces.Services;

public interface ICartService
{
    Task AddToCartAsync(int userId, AddToCartDto dto);
    Task UpdateCartItemAsync(int itemId, UpdateCartItemDto dto);
    Task RemoveFromCartAsync(int itemId);
    Task<IEnumerable<CartItemDto>> GetCartItemsAsync(int userId);

}
