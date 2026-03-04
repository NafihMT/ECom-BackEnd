using ECom.Application.DTOs.Wishlist;
using ECom.Application.DTOs.Product; 
using ECom.Application.DTOs.Category; 
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
        // FIX: Check for duplicates before adding
        var existingItems = await _wishlistRepository.GetByUserAsync(userId);
        if (existingItems.Any(i => i.ProductId == productId))
        {
            // Return existing item or handle as a custom exception
            var existing = existingItems.First(i => i.ProductId == productId);
            return new WishlistItemDto { Id = existing.Id, ProductId = existing.ProductId };
        }

        var item = await _wishlistRepository.AddAsync(userId, productId);

        return new WishlistItemDto
        {
            Id = item.Id,
            ProductId = item.ProductId
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
                ImageUrl = i.Product.ImageUrl,
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