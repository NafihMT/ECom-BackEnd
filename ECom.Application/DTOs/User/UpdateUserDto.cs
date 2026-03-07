using ECom.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ECom.Application.DTOs.User;

public class UpdateUserDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [Phone]
    public string PhoneNo { get; set; }

    [Required]
    public string Role { get; set; }

    public bool IsBlocked { get; set; }
}