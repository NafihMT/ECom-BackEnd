using ECom.Domain.Entities;
using ECom.Domain.Interfaces.Repositories;
using ECom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECom.Infrastructure.Repositories;

public class WishlistRepository : IWishlistRepository
{
    private readonly AppDbContext _context;

    public WishlistRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(int userId, int productId)
    {
        var wishlist = await _context.Wishlists
            .Include(w => w.Items)
            .FirstOrDefaultAsync(w => w.UserId == userId);

        if (wishlist == null)
        {
            wishlist = new Wishlist { UserId = userId };
            _context.Wishlists.Add(wishlist);
        }

        wishlist.Items ??= new List<WishlistItem>();

        wishlist.Items.Add(new WishlistItem { ProductId = productId });

        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<object>> GetByUserAsync(int userId)
    {
        return await _context.WishlistItems
            .Include(wi => wi.Product)
            .Where(wi => wi.Wishlist.UserId == userId)
            .Select(wi => new
            {
                wi.Id,
                wi.Product.Name,
                wi.Product.Price
            })
            .ToListAsync<object>();
    }

    public async Task RemoveAsync(int wishlistItemId)
    {
        var item = await _context.WishlistItems.FindAsync(wishlistItemId);
        if (item == null) return;

        _context.WishlistItems.Remove(item);
        await _context.SaveChangesAsync();
    }
}

