using ECom.Application.Common;
using ECom.Application.DTOs.Product;
using ECom.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECom.Api.Controllers;

[ApiController]
[Route("api/product")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("GetAll-Product")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _productService.GetAllAsync());
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetById(int productId)
    {
        return Ok(await _productService.GetByIdAsync(productId));
    }

    [HttpGet("category/{category}")]
    public async Task<IActionResult> GetByCategory(string category)
    {
        return Ok(await _productService.GetByCategoryAsync(category));
    }

    [HttpGet("search/{searchString}")]
    public async Task<IActionResult> Search(string searchString)
    {
        return Ok(await _productService.SearchAsync(searchString));
    }

    [HttpGet("paginated")]
    public async Task<IActionResult> Paginated(int pageNumber, int pageSize)
    {
        return Ok(await _productService.GetPaginatedAsync(pageNumber, pageSize));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("Add-Product")]
    public async Task<IActionResult> Add(CreateProductDto dto)
    {
        var Product = await _productService.AddAsync(dto);
        return Ok(new ApiResponse<object>(
            StatusCodes.Status200OK,
            "Product Added sucessfully",
            Product
            ));
    }
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto dto)
    {
        var updatedProduct = await _productService.UpdateAsync(id, dto);
        return Ok(new ApiResponse<object>(
            StatusCodes.Status200OK,
            "Product Updated",
            updatedProduct
            )); ;
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.DeleteAsync(id);
        return Ok(new ApiResponse<object>(
            StatusCodes.Status200OK,
            "Product Deleted successfully",
            null
        ));
    }
}
