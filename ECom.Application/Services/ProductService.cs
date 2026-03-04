using AutoMapper;
using ECom.Application.DTOs.Product;
using ECom.Application.Interfaces.Services;
using ECom.Domain.Interfaces.Repositories;
using ECom.Domain.Entities;

namespace ECom.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;


    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }



    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> UpdateAsync(int id, UpdateProductDto dto)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
            throw new Exception("Product not found");

        _mapper.Map(dto, product);

        try
        {
            await _productRepository.UpdateAsync(product);
        }
        catch (Exception ex)
        {
            throw new Exception($"Database update failed: {ex.InnerException?.Message ?? ex.Message}");
        }

        return _mapper.Map<ProductDto>(product);
    }



    public async Task<IEnumerable<ProductDto>> GetByCategoryAsync(string category)
    {
        var products = await _productRepository.GetByCategoryAsync(category);
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }


    public async Task<IEnumerable<ProductDto>> SearchAsync(string search)
    {
        var products = await _productRepository.SearchAsync(search);
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<PaginatedProductDto> GetPaginatedAsync(int pageNumber, int pageSize)
    {
        var (items, totalCount) =
            await _productRepository.GetPaginatedAsync(pageNumber, pageSize);

        return new PaginatedProductDto
        {
            Products = _mapper.Map<IEnumerable<ProductDto>>(items),
            TotalCount = totalCount
        };
    }


    public async Task<ProductDto> AddAsync(CreateProductDto dto)
    {
        var product = _mapper.Map<Product>(dto);

        if (product == null)
            throw new Exception("Invalid product data");

        await _productRepository.AddAsync(product);

        var createdProduct = await _productRepository.GetByIdAsync(product.Id);

        return _mapper.Map<ProductDto>(createdProduct);
    }
    public async Task DeleteAsync(int id)
    {
        await _productRepository.DeleteAsync(id);
    }
}
