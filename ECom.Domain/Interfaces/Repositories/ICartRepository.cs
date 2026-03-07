using ECom.Domain.Entities;

namespace ECom.Domain.Interfaces.Repositories;

public interface ICartRepository
{
    Task AddItemAsync(int userId, int productId, int quantity);
    Task UpdateQuantityAsync(int cartItemId, int quantity);
    Task RemoveFromCartAsync(int cartItemId);
    Task<IEnumerable<CartItem>> GetItemsByUserAsync(int userId);

}
