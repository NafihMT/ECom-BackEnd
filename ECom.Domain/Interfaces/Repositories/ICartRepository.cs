namespace ECom.Domain.Interfaces.Repositories;

public interface ICartRepository
{
    Task AddItemAsync(int userId, int productId, int quantity);
    Task UpdateQuantityAsync(int cartItemId, int quantity);
    Task RemoveFromCartAsync(int cartItemId);
    Task<IEnumerable<object>> GetItemsByUserAsync(int userId);
}
