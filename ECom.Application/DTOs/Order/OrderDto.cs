
using ECom.Domain.Enums;

namespace ECom.Application.DTOs.Order;

public class OrderDto
{
    public int Id { get; set; }
    public OrderStatus Status { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public IEnumerable<OrderItemDto> Items { get; set; }
}