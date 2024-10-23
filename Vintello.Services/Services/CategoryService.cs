using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;
using Vintello.Common.Repositories;

namespace Vintello.Services;

public class CategoryService(ICategoryRepository repo) : ICategoryService
{
    public async Task<RetrievedCategoryDto?> CreateAsync(CreatedCategoryDto categoryDto)
    {
        Category? category = await repo.CreateAsync(CreatedCategoryDto.CreateCategory(categoryDto));
        return category is not null ? RetrievedCategoryDto.CreateDto(category) : null;
    }

    public async Task<IEnumerable<RetrievedCategoriesDto>> RetrieveAsync()
    {
        IEnumerable<Category> categories = await repo.RetrieveAllAsync();
        return RetrievedCategoriesDto.CreateDto(categories);
    }

    public async Task<RetrievedCategoryDto?> RetrieveByIdAsync(int id)
    {
        Category? category = await repo.RetrieveByIdAsync(id);
        return category is null ? null : RetrievedCategoryDto.CreateDto(category);
    }

    public async Task<bool?> UpdateAsync(int id, UpdatedCategoryDto category)
    {
        Category? existing = await repo.RetrieveByIdAsync(id);
        if (existing is null) return null;
        Category updatedCategory = UpdatedCategoryDto.CreateCategory(category, existing);
        Category? updated = await repo.UpdateAsync(id, updatedCategory);
        return updated is not null;
    }

    public async Task<bool?> DeleteAsync(int id)
    {
        Category? category = await repo.RetrieveByIdAsync(id);
        if (category is null) return null;
        return await repo.DeleteAsync(category);
    }
}