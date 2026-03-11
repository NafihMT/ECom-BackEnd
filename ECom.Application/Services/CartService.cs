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


    public async Task<CartItemDto> UpdateCartItemAsync(int itemId, UpdateCartItemDto dto)
    {
        await _cartRepository.UpdateQuantityAsync(itemId, dto.Quantity);

        var item = await _cartRepository.GetByIdAsync(itemId);
        if (item == null) throw new KeyNotFoundException("Cart item not found");

        return new CartItemDto
        {
            Id = item.Id,
            ProductId = item.ProductId,
            Quantity = item.Quantity,
            Name = item.Product?.Name,
            Price = item.Product?.Price ?? 0,
            ImageUrl = item.Product?.ImageUrl,
            Category = item.Product?.Category?.Name
        };
    }

    public async Task RemoveFromCartAsync(int itemId)
    {
        await _cartRepository.RemoveFromCartAsync(itemId);
    }
    public async Task<IEnumerable<CartItemDto>> GetCartItemsAsync(int userId)
    {
        var cartItems = await _cartRepository.GetItemsByUserAsync(userId);

        if (cartItems == null)
            return new List<CartItemDto>();

        return cartItems.Select(item => new CartItemDto
        {
            Id = item.Id,
            ProductId = item.ProductId,
            Quantity = item.Quantity,
            Name = item.Product?.Name,
            Price = item.Product?.Price ?? 0,
            ImageUrl = item.Product?.ImageUrl,
            Category = item.Product?.Category?.Name
        });
    }


}
