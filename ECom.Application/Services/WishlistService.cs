using ECom.Application.DTOs.Wishlist;
using ECom.Application.DTOs.Product; // Ensure you have this namespace
using ECom.Application.DTOs.Category; // And this one
using ECom.Application.Interfaces.Services;
using ECom.Domain.Interfaces.Repositories;

namespace ECom.Application.Services;

public class WishlistService : IWishlistService
{
    private readonly IWishlistRepository _wishlistRepository;

    public WishlistService(IWishlistRepository wishlistRepository)
    {
        _wishlistRepository = wishlistRepository;
    }

    public async Task<WishlistItemDto> AddToWishlistAsync(int userId, int productId)
    {
        var item = await _wishlistRepository.AddAsync(userId, productId);

        return new WishlistItemDto
        {
            Id = item.Id,
            ProductId = item.ProductId
            // We don't necessarily need the full product details on "Add" response 
            // since the frontend re-fetches the list anyway.
        };
    }

    public async Task<IEnumerable<WishlistItemDto>> GetWishlistAsync(int userId)
    {
        var items = await _wishlistRepository.GetByUserAsync(userId);

        // FIX: Map the Product Entity to the DTO
        return items.Select(i => new WishlistItemDto
        {
            Id = i.Id,
            ProductId = i.ProductId,
            Product = i.Product == null ? null : new ProductDto
            {
                Id = i.Product.Id,
                Name = i.Product.Name,
                Price = i.Product.Price,
                Image = i.Product.Image,
                Description = i.Product.Description,
                Category = i.Product.Category == null ? null : new CategoryDto
                {
                    Id = i.Product.Category.Id,
                    Name = i.Product.Category.Name
                }
            }
        });
    }

    public async Task RemoveFromWishlistAsync(int itemId)
    {
        await _wishlistRepository.RemoveAsync(itemId);
    }
}