using ECom.Application.DTOs.Product;

namespace ECom.Application.Interfaces.Services;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task<ProductDto> GetByIdAsync(int id);
    Task<IEnumerable<ProductDto>> GetByCategoryAsync(string category);
    Task<IEnumerable<ProductDto>> SearchAsync(string search);
    Task<PaginatedProductDto> GetPaginatedAsync(int pageNumber, int pageSize);

    Task AddAsync(CreateProductDto dto);
}
