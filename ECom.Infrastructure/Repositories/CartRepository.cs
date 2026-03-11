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
            throw new ArgumentException("Invalid product id");

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

        var existingItems = cart.Items.Where(i => i.ProductId == productId).ToList();

        if (existingItems.Any())
        {
            var primaryItem = existingItems.First();
            primaryItem.Quantity += quantity;

            if (existingItems.Count > 1)
            {
                foreach (var duplicate in existingItems.Skip(1))
                {
                    primaryItem.Quantity += duplicate.Quantity;
                    _context.CartItems.Remove(duplicate);
                }
            }
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

        if (item == null)
            throw new KeyNotFoundException("Cart item not found");

        item.Quantity = quantity;

        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<CartItem>> GetItemsByUserAsync(int userId)
    {
        return await _context.CartItems
            .Include(ci => ci.Product)
                .ThenInclude(p => p.Category)
            .Include(ci => ci.Cart)   
            .Where(ci => ci.Cart.UserId == userId)
            .ToListAsync();
    }


    public async Task RemoveFromCartAsync(int cartItemId)
    {
        var item = await _context.CartItems.FindAsync(cartItemId);

        if (item == null)
            throw new KeyNotFoundException("Cart item not found");

        _context.CartItems.Remove(item);

        await _context.SaveChangesAsync();
    }
    public async Task<CartItem?> GetByIdAsync(int itemId)
    {
        return await _context.CartItems
            .Include(ci => ci.Product)
                .ThenInclude(p => p.Category)
            .FirstOrDefaultAsync(ci => ci.Id == itemId);
    }
}
