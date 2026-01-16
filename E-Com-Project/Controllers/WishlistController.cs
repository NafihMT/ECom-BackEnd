using ECom.Application.Common;
using ECom.Application.Interfaces.Services;
using ECom.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Authorize]
[Route("api/wishlist")]
public class WishlistController : ControllerBase
{
    private readonly IWishlistService _wishlistService;

    public WishlistController(IWishlistService wishlistService)
    {
        _wishlistService = wishlistService;
    }

    private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

    // FIX: Removed "GetAll-Wishlist" to match frontend call /api/wishlist
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var wishlist = await _wishlistService.GetWishlistAsync(UserId);
        return Ok(new ApiResponse<object>(200, "Fetched Successfully", wishlist));
    }

    [HttpPost("{productId}")]
    public async Task<IActionResult> Add(int productId)
    {
        var addedItem = await _wishlistService.AddToWishlistAsync(UserId, productId);
        return Ok(new ApiResponse<object>(200, "Added Successfully", addedItem));
    }

    [HttpDelete("remove/{itemId}")]
    public async Task<IActionResult> Delete(int itemId)
    {
        await _wishlistService.RemoveFromWishlistAsync(itemId);
        return Ok(new ApiResponse<object>(200, "Deleted Successfully", null));
    }
}