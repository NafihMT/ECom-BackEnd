using ECom.Application.DTOs.User;
using System.Threading.Tasks;

namespace ECom.Application.Interfaces.Services;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByIdAsync(int id);
    Task<UserDto> AddUserAsync(AddUserDto dto);
    Task<UserDto> UpdateUserAsync(int id, UpdateUserDto dto);
    Task<UserDto> UpdateUserStatusAsync(int id, bool isBlocked);
    Task DeleteUserAsync(int id);
}