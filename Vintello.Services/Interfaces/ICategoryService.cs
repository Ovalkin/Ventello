using Vintello.Common.DTOs;

namespace Vintello.Services;

public interface ICategoryService
{
    Task<RetrivedCategoryDto?> CreateAsync(CreatedCategoryDto category);
    Task<RetrivedCategoryDto?> RetrieveByIdAsync(int id);
    Task<IEnumerable<RetrivedCategoriesDto>> RetrieveAsync();
    Task<bool?> UpdateAsync(int id, UpdatedCategoryDto category);
    Task<bool?> DeleteAsync(int id);
}