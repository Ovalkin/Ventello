using Vintello.Common.DTOs;

namespace Vintello.Services;

public interface ICategoryService
{
    Task<RetrivedCategoryDto?> RetriveByIdAsync(int id);
    Task<RetrivedCategoryDto?> CreateAsync(CreatedUpdatedCategoryDto categoryDto);
    Task<IEnumerable<RetrivedCategoriesDto>> RetriveAllAsync();
    Task<bool?> UpdateAsync(int id, CreatedUpdatedCategoryDto category);
    Task<bool?> DeleteAsync(int id);
}