using Vintello.Common.DTOs;

namespace Vintello.Services;

public interface ICategoryService
{
    Task<RetrivedCategoryDto?> CreateAsync(CreatedCategoryDto categoryDto);
    Task<RetrivedCategoryDto?> RetriveByIdAsync(int id);
    Task<IEnumerable<RetrivedCategoriesDto>> RetriveAsync();
    Task<bool?> UpdateAsync(int id, UpdatedCategoryDto category);
    Task<bool?> DeleteAsync(int id);
}