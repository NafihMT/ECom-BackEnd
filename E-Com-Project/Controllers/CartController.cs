using ECom.Application.Common;
using ECom.Application.DTOs.Cart;
using ECom.Application.Interfaces.Services;
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

    private int UserId =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public async Task<IActionResult> GetCart()
    {
        var result = await _cartService.GetCartItemsAsync(UserId);
        return Ok(result);
    }



    [HttpPost("{productId}")]
    public async Task<IActionResult> AddToCart(int productId, AddToCartDto dto)
    {
        dto.ProductId = productId;

        await _cartService.AddToCartAsync(UserId, dto);

        return Ok(new ApiResponse<object>(200, "Added to cart", dto));
    }

    [HttpPut("update/{itemId}")]
    public async Task<IActionResult> UpdateItem(int itemId, UpdateCartItemDto dto)
    {
        await _cartService.UpdateCartItemAsync(itemId, dto);
        return Ok(new ApiResponse<object>(200, "Updated successfully", dto));
    }

    [HttpDelete("remove/{itemId}")]
    public async Task<IActionResult> Delete(int itemId)
    {
        await _cartService.RemoveFromCartAsync(itemId);
        return Ok(new ApiResponse<object>(200, "Removed successfully", ""));
    }
}
