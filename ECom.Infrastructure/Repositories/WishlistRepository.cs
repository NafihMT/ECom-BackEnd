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

    public async Task<WishlistItem> AddAsync(int userId, int productId)
    {
        var wishlist = await _context.Wishlists
            .Include(w => w.Items)
            .FirstOrDefaultAsync(w => w.UserId == userId);

        if (wishlist == null)
        {
            wishlist = new Wishlist
            {
                UserId = userId,
                Items = new List<WishlistItem>()
            };
            _context.Wishlists.Add(wishlist);
        }

        var existingItem = wishlist.Items.FirstOrDefault(i => i.ProductId == productId);
        if (existingItem != null)
        {
            return existingItem;
        }

        var item = new WishlistItem
        {
            ProductId = productId
        };

        wishlist.Items.Add(item);

        await _context.SaveChangesAsync();

        return item;
    }

    public async Task<IEnumerable<WishlistItem>> GetByUserAsync(int userId)
    {
        return await _context.WishlistItems
            .Include(wi => wi.Product)
                .ThenInclude(p => p.Category) 
            .Include(wi => wi.Wishlist)
            .Where(wi => wi.Wishlist.UserId == userId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task RemoveAsync(int wishlistItemId)
    {
        var item = await _context.WishlistItems.FindAsync(wishlistItemId);
        if (item == null)
        {
            return;
        }

        _context.WishlistItems.Remove(item);
        await _context.SaveChangesAsync();
    }
}