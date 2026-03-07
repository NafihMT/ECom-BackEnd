using ECom.Application.Common;
using ECom.Application.DTOs.User;
using ECom.Application.Interfaces.Services;
using ECom.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/user")]
[ApiController]
[Authorize(Roles = "Admin")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]

    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
                    
        return Ok(new ApiResponse<object>(
            StatusCodes.Status200OK,
            "Users Fetched Successfully",
            users
            ));
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
                    
        if (user == null)
            return NotFound("User Not Found");
        return Ok(new ApiResponse<object>(
            StatusCodes.Status200OK,
            "User Fetched Successfully",
            user
            ));
    }

    [HttpPut("{id}/status")]

    public async Task<IActionResult> UpdateUserStatus(int id, UpdateUserStatusDto dto)
    {

        var result = await _userService.UpdateUserStatusAsync(id, dto.IsBlocked);
        return Ok(new ApiResponse<object>(
            200,
            "User Status Updated",
            result
            ));
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(AddUserDto dto)
    {
        var result = await _userService.AddUserAsync(dto);

        return Ok(new ApiResponse<object>(
            StatusCodes.Status200OK,
            "User Added Successfully",
            result
        ));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, UpdateUserDto dto)
    {
        var result = await _userService.UpdateUserAsync(id, dto);

        return Ok(new ApiResponse<object>(
            StatusCodes.Status200OK,
            "User Updated Successfully",
            result
        ));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _userService.DeleteUserAsync(id);
        return Ok(new { message = "User deleted successfully" });
    }




}