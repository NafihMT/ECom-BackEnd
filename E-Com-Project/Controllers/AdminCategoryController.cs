using ECom.Application.Common;
using ECom.Application.DTOs.Category;
using ECom.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECom.Api.Controllers;

[ApiController]
[Route("api/admin/categories")]
[Authorize(Roles = "Admin")]
public class AdminCategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public AdminCategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost("Add-Category")]
    public async Task<IActionResult> Add(CreateCategoryDto dto)
    {
        var Category = await _categoryService.AddAsync(dto);

        return Ok(new ApiResponse<CategoryDto>(
            StatusCodes.Status200OK,
            "Category added successfully",
            Category
        ));
    }

    [HttpPut("Update-Category")]
    public async Task<IActionResult> Update(UpdateCategoryDto dto)
    {
        var Updated = await _categoryService.UpdateAsync(dto);
        return Ok(new ApiResponse<object>(
            StatusCodes.Status200OK,
            "Category Updated Successfully",
            Updated
            ));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var Deleted = await _categoryService.DeleteAsync(id);
        return Ok(new ApiResponse<object>(
            StatusCodes.Status200OK,
            "Deleted Successfully",
            Deleted
            ));
    }
}
