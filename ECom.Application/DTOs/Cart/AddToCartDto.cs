using System.ComponentModel.DataAnnotations;

namespace ECom.Application.DTOs.Cart;

public class AddToCartDto
{

    [Required]
    public int ProductId { get; set; }

    [Required]
    public int Quantity { get; set; }
}
