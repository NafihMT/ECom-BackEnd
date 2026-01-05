using ECom.Application.DTOs.Category;
using ECom.Application.Interfaces.Services;
using ECom.Domain.Entities;
using ECom.Domain.Interfaces.Repositories;

namespace ECom.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();

        return categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name
        });
    }

    public async Task AddAsync(CreateCategoryDto dto)
    {
        var category = new Category
        {
            Name = dto.Name,
            
        };

        await _categoryRepository.AddAsync(category);
    }

    public async Task UpdateAsync(UpdateCategoryDto dto)
    {
        var category = await _categoryRepository.GetByIdAsync(dto.Id)
            ?? throw new Exception("Category not found");

        category.Name = dto.Name;
        await _categoryRepository.UpdateAsync(category);
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id)
            ?? throw new Exception("Category not found");

        await _categoryRepository.DeleteAsync(category);
    }
}
