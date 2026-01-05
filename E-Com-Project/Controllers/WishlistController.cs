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
        await _wishlistService.AddToWishlistAsync(UserId, productId);
        return Ok(new { status = "success" });
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _wishlistService.GetWishlistAsync(UserId));
    }

    [HttpDelete("{itemId}")]
    public async Task<IActionResult> Delete(int itemId)
    {
        await _wishlistService.RemoveFromWishlistAsync(itemId);
        return Ok(new { status = "success" });
    }
}
