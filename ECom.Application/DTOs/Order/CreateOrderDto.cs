using ECom.Application.DTOs.Order;

namespace ECom.Application.DTOs.Order;

public class CreateOrderDto
{
    public IEnumerable<CreateOrderItemDto> Items { get; set; }
    public ShippingAddressDto ShippingAddress { get; set; } 
}