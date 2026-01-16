using ECom.Application.DTOs.Category;

namespace ECom.Application.Interfaces.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllAsync();
    Task<CategoryDto> AddAsync(CreateCategoryDto dto);
    Task<CategoryDto> UpdateAsync(UpdateCategoryDto dto);
    Task<CategoryDto> DeleteAsync(int id);
}
