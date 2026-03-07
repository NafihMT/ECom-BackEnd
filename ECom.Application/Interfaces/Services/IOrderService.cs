using ECom.Application.DTOs.Order;
using ECom.Domain.Enums;


namespace ECom.Application.Interfaces.Services;

public interface IOrderService
{
    Task<OrderDto> PlaceOrderAsync(int userId, CreateOrderDto dto);
    Task<OrderDto?> GetByIdAsync(int id);
    Task<IEnumerable<OrderDto>> GetByUserAsync(int userId);
    Task<OrderDto> UpdateStatusAsync(int orderId, OrderStatus status);

    //Task<IEnumerable<OrderDto>> GetAllAsync();

    Task<decimal> GetTotalRevenueAsync();
}
