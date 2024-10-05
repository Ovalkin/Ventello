using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;
using Vintello.Common.Repositories;

namespace Vintello.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repo;

    public CategoryService(ICategoryRepository repo)
    {
        _repo = repo;
    }

    public async Task<RetrievedCategoryDto?> CreateAsync(CreatedCategoryDto categoryDto)
    {
        Category? category = await _repo.CreateAsync(CreatedCategoryDto.CreateCategory(categoryDto));
        if (category is not null) return  RetrievedCategoryDto.CreateDto(category);
        return null;
    }

    public async Task<IEnumerable<RetrievedCategoriesDto>> RetrieveAsync()
    {
        var categories = await _repo.RetrieveAllAsync();
        return RetrievedCategoriesDto.CreateDto(categories);
    }

    public async Task<RetrievedCategoryDto?> RetrieveByIdAsync(int id)
    {
        Category? category = await _repo.RetrieveByIdAsync(id);
        if (category is null) return null;
        return RetrievedCategoryDto.CreateDto(category);
    }

    public async Task<bool?> UpdateAsync(int id, UpdatedCategoryDto category)
    {
        Category? existing = await _repo.RetrieveByIdAsync(id);
        if (existing is null) return null;

        Category updatedCategory = UpdatedCategoryDto.CreateCategory(category, existing);

        Category? updated = await _repo.UpdateAsync(id, updatedCategory);
        if (updated is null) return false;
        return true;
    }

    public async Task<bool?> DeleteAsync(int id)
    {
        Category? category = await _repo.RetrieveByIdAsync(id);
        if (category is null) return null;
        return await _repo.DeleteAsync(category);
    }
}