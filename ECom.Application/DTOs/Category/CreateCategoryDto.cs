using System.ComponentModel.DataAnnotations;

namespace ECom.Application.DTOs.Category;

public class CreateCategoryDto
{

    [Required]
    public string Name { get; set; }
}
