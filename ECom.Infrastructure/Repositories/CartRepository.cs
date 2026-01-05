using ECom.Domain.Entities;
using ECom.Domain.Interfaces.Repositories;
using ECom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECom.Infrastructure.Repositories;

public class CartRepository : ICartRepository
{
    private readonly AppDbContext _context;

    public CartRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddItemAsync(int userId, int productId, int quantity)
    {
        var productExists = await _context.Products
            .AnyAsync(p => p.Id == productId);

        if (!productExists)
        {
            throw new ArgumentException("Invalid product id");
        }

        var cart = await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null)
        {
            cart = new Cart
            {
                UserId = userId,
                Items = new List<CartItem>()
            };

            _context.Carts.Add(cart);
        }

        var existingItem = cart.Items
            .FirstOrDefault(i => i.ProductId == productId);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            cart.Items.Add(new CartItem
            {
                ProductId = productId,
                Quantity = quantity
            });
        }

        await _context.SaveChangesAsync();
    }


    public async Task UpdateQuantityAsync(int cartItemId, int quantity)
    {
        var item = await _context.CartItems.FindAsync(cartItemId);
        if (item == null) return;

        item.Quantity = quantity;
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<object>> GetItemsByUserAsync(int userId)
    {
        return await _context.CartItems
            .Include(ci => ci.Product)
            .Where(ci => ci.Cart.UserId == userId)
            .Select(ci => new
            {
                ci.Id,
                ci.Product.Name,
                ci.Quantity,
                ci.Product.Price
            })
            .ToListAsync<object>();
    }

    public async Task RemoveFromCartAsync(int cartItemId)
    {
        var item = await _context.CartItems.FindAsync(cartItemId);
        if (item == null)
            throw new KeyNotFoundException("Cart item not found");

        _context.CartItems.Remove(item);
        await _context.SaveChangesAsync();
    }
}


