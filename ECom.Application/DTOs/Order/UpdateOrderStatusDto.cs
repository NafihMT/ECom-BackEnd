
using ECom.Domain.Enums;

namespace ECom.Application.DTOs.Order;

public class UpdateOrderStatusDto
{
    public OrderStatus Status { get; set; }
}