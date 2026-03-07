using ECom.Domain.Entities;

namespace ECom.Domain.Interfaces.Services;

public interface IJwtService
{
    string GenerateToken(User user);
    string GenerateRefreshToken();
}
