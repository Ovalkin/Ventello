using Vintello.Common.DTOs;

namespace Vintello.Services;

public interface ICategoryService
{
    Task<RetrivedCategoryDto?> RetriveByIdAsync(int id);
    Task<RetrivedCategoryDto?> CreateAsync(CreatedCategoryDto categoryDto);
    Task<IEnumerable<RetrivedCategoriesDto>> RetriveAllAsync();
    Task<bool?> UpdateAsync(int id, UpdatedCategoryDto category);
    Task<bool?> DeleteAsync(int id);
}