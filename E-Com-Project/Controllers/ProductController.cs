using ECom.Application.DTOs.Product;
using ECom.Application.Interfaces.Services;
using ECom.Application.Services;
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

    [HttpGet("getAll")]
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
    [HttpPost]
    public async Task<IActionResult> Add(CreateProductDto dto)
    {
        await _productService.AddAsync(dto);
        return Ok(new { message = "Product added successfully" });
    }
}
