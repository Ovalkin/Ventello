using Vintello.Common.DTOs;

namespace Vintello.Services;

public interface ICategoryService
{
    Task<RetrievedCategoryDto?> CreateAsync(CreatedCategoryDto category);
    Task<RetrievedCategoryDto?> RetrieveByIdAsync(int id);
    Task<IEnumerable<RetrievedCategoriesDto>> RetrieveAsync();
    Task<bool?> UpdateAsync(int id, UpdatedCategoryDto category);
    Task<bool?> DeleteAsync(int id);
}