using ECom.Application.DTOs.Cart;
using ECom.Application.Interfaces.Services;
using ECom.Domain.Interfaces.Repositories;

namespace ECom.Application.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;

    public CartService(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task AddToCartAsync(int userId, AddToCartDto dto)
    {
        if (dto.Quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero");

        await _cartRepository.AddItemAsync(userId, dto.ProductId, dto.Quantity);
    }

    public async Task UpdateCartItemAsync(int itemId, UpdateCartItemDto dto)
    {
        await _cartRepository.UpdateQuantityAsync(itemId, dto.Quantity);
    }

    public async Task RemoveFromCartAsync(int itemId)
    {
        await _cartRepository.RemoveFromCartAsync(itemId);
    }
    public async Task<IEnumerable<object>> GetCartItemsAsync(int userId)
    {
        return await _cartRepository.GetItemsByUserAsync(userId);
    }
}
