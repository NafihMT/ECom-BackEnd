using ECom.Application.DTOs.Order;

public class OrderDto
{
    public int Id { get; set; }

    public string Status { get; set; } = null!;

    public decimal TotalAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public IEnumerable<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
    public ShippingAddressDto ShippingAddress { get; set; } 
}
