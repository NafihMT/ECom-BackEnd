using ECom.Application.Common;
using ECom.Application.Interfaces.Services;
using ECom.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECom.Api.Controllers;

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

    private int UserId =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

    [HttpPost("{productId}")]
    public async Task<IActionResult> Add(int productId)
    {
        var addedItem = await _wishlistService.AddToWishlistAsync(UserId, productId);

        return Ok(new ApiResponse<object>(
            StatusCodes.Status200OK,
            "Added Successfully",
            addedItem
        ));
    }

    [HttpGet("GetAll-Wishlist")]
    public async Task<IActionResult> GetAll()
    {
        var wishlist = await _wishlistService.GetWishlistAsync(UserId);

        return Ok(new ApiResponse<object>(
            StatusCodes.Status200OK,
            "Fetched Successfully",
            wishlist
            ));
    }

    [HttpDelete("{itemId}")]
    public async Task<IActionResult> Delete(int itemId)
    {
        await _wishlistService.RemoveFromWishlistAsync(itemId);
        return Ok(new ApiResponse<object>(
            StatusCodes.Status200OK,
            "Deleted "
            ));
        
    }
    
}


