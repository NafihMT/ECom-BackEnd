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

    public async Task AddToWishlistAsync(int userId, int productId)
    {
        await _wishlistRepository.AddAsync(userId, productId);
    }

    public async Task<IEnumerable<object>> GetWishlistAsync(int userId)
    {
        return await _wishlistRepository.GetByUserAsync(userId);
    }

    public async Task RemoveFromWishlistAsync(int itemId)
    {
        await _wishlistRepository.RemoveAsync(itemId);
    }
}
