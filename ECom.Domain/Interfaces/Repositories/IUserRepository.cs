using ECom.Domain.Entities;

namespace ECom.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User> GetByUsernameAsync(string username);
    Task<User> GetByIdAsync(int id);
    Task AddAsync(User user);
    Task<User?> GetByRefreshTokenAsync(string refreshToken);

    Task UpdateAsync(User user);
}
