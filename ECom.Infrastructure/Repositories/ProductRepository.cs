using ECom.Domain.Entities;
using ECom.Domain.Interfaces.Repositories;
using ECom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECom.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products
            .Include(p => p.Category)
            .ToListAsync();
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> GetByCategoryAsync(string category)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Where(p => p.Category.Name == category)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> SearchAsync(string search)
    {
        return await _context.Products
            .Include(p => p.Category)
        .Where(p =>
            p.Name.Contains(search) ||
            p.Description.Contains(search) ||
            p.Category.Name.Contains(search))
            .ToListAsync();
    }

    public async Task<(IEnumerable<Product>, int)> GetPaginatedAsync(int page, int size)
    {
        var query = _context.Products.Include(p => p.Category);

        var total = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync();

        return (items, total);
    }

    public async Task AddAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

}
