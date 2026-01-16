using ECom.Application.DTOs.Product;

namespace ECom.Application.DTOs.Wishlist;

public class WishlistItemDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public ProductDto Product { get; set; }
}
