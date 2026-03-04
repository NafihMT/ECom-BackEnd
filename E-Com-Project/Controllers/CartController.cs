using ECom.Application.Common;
using ECom.Application.DTOs.Cart;
using ECom.Application.Interfaces.Services;
using ECom.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Authorize]
[Route("api/cart")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

    // FIX: Removed "GetAll-Cart" so the frontend can just call /api/cart
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _cartService.GetCartItemsAsync(UserId);
        return Ok(new ApiResponse<IEnumerable<object>>(200, "Fetched Successfully", items));
    }

    [HttpPost("{productId}")]
    public async Task<IActionResult> AddToCart(int productId, AddToCartDto dto)
    {
        dto.ProductId = productId;
        await _cartService.AddToCartAsync(UserId, dto);
        return Ok(new ApiResponse<object>(200, "Added to cart", null));
    }

    // FIX: Route updated to /api/cart/update/{itemId} to match standard naming
    [HttpPut("update/{itemId}")]
    public async Task<IActionResult> UpdateItem(int itemId, UpdateCartItemDto dto)
    {
        await _cartService.UpdateCartItemAsync(itemId, dto);
        return Ok(new ApiResponse<object>(200, "Updated successfully", null));
    }

    [HttpDelete("remove/{itemId}")]
    public async Task<IActionResult> Delete(int itemId)
    {
        await _cartService.RemoveFromCartAsync(itemId);
        return Ok(new ApiResponse<object>(200, "Removed successfully", null));
    }
}