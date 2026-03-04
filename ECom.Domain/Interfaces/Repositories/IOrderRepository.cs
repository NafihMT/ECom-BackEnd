using ECom.Domain.Entities;


namespace ECom.Domain.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<Order> CreateAsync(Order order);
    Task<Order?> GetByIdAsync(int id);
    Task<IEnumerable<Order>> GetByUserAsync(int userId);
    Task UpdateAsync(Order order);
}