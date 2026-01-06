using System.ComponentModel.DataAnnotations;

namespace ECom.Application.DTOs.Cart;

public class UpdateCartItemDto
{

    [Required]
    public int Quantity { get; set; }
}
