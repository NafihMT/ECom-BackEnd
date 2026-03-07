using AutoMapper;
using ECom.Application.DTOs.User;
using ECom.Application.Interfaces.Services;
using ECom.Domain.Entities;
using ECom.Domain.Enums;
using ECom.Domain.Interfaces.Repositories;

namespace ECom.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                PhoneNo = u.PhoneNo,
                Role = u.Role.ToString(),
                IsBlocked = u.IsBlocked
            });
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return null;

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNo = user.PhoneNo,
                Role = user.Role.ToString(),
                IsBlocked = user.IsBlocked
            };
        }

        public async Task<UserDto> AddUserAsync(AddUserDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PhoneNo = dto.PhoneNo,
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role,
                IsBlocked = false
            };

            await _userRepository.AddAsync(user);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateUserAsync(int id, UpdateUserDto dto)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                throw new KeyNotFoundException("User not found");

            //var existingUser = await _userRepository.GetByUsernameAsync(dto.Username);
            //if (existingUser != null && existingUser.Id != id)
            //    throw new Exception("Username already exists");

            user.Name = dto.Name;
            //user.Username = dto.Username;
            user.Email = dto.Email;
            user.PhoneNo = dto.PhoneNo;
            user.Role = Enum.Parse<UserRole>(dto.Role);

            await _userRepository.UpdateAsync(user);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateUserStatusAsync(int id, bool isBlocked)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                throw new Exception("User not found");

            user.IsBlocked = isBlocked;

            if (isBlocked)
            {
                user.RefreshToken = null;
                user.RefreshTokenExpiryTime = null;
            }

            await _userRepository.UpdateAsync(user);

            return _mapper.Map<UserDto>(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                throw new Exception("User not found");

            user.IsBlocked = true;

            await _userRepository.UpdateAsync(user);
        }
    }

}