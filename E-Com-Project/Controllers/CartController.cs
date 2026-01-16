using ECom.Application.DTOs.Cart;
using ECom.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECom.Api.Controllers;

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

    private int UserId =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

    [HttpPost("{productId}")]
    public async Task<IActionResult> AddToCart(int productId, AddToCartDto dto)
    {
        dto.ProductId = productId;
        await _cartService.AddToCartAsync(UserId, dto);
        return Ok(new { status = "success" });
    }

    [HttpPut("item/{itemId}")]
    public async Task<IActionResult> UpdateItem(int itemId, UpdateCartItemDto dto)
    {
        await _cartService.UpdateCartItemAsync(itemId, dto);
        return Ok(new { status = "success" });
    }

    [HttpGet("GetAll-Cart")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _cartService.GetCartItemsAsync(UserId));
    }

    [HttpDelete("{itemId}")]
    public async Task<IActionResult> Delete(int itemId)
    {
        await _cartService.RemoveFromCartAsync(itemId);
        return Ok(new { status = "success" });
    }
}
