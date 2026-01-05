using ECom.Domain.Entities;

namespace ECom.Domain.Interfaces.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(int id);
    Task<IEnumerable<Product>> GetByCategoryAsync(string category);
    Task<IEnumerable<Product>> SearchAsync(string search);
    Task<(IEnumerable<Product>, int)> GetPaginatedAsync(int page, int size);

    Task AddAsync(Product product);
}
