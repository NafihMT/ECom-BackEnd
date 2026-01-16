using ECom.Application.DTOs.Order;


namespace ECom.Application.Interfaces.Services;

public interface IOrderService
{
    Task<OrderDto> PlaceOrderAsync(int userId, CreateOrderDto dto);
    Task<OrderDto?> GetByIdAsync(int id);
    Task<IEnumerable<OrderDto>> GetByUserAsync(int userId);
    Task UpdateStatusAsync(int orderId, ECom.Domain.Enums.OrderStatus status);
}