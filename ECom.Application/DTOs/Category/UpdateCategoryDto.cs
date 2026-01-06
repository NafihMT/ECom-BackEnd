using System.ComponentModel.DataAnnotations;

namespace ECom.Application.DTOs.Category;

public class UpdateCategoryDto
{

    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
}
