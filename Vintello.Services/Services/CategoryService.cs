using Vintello.Common.DTOs;
using AutoMapper;
using Vintello.Common.EntityModel.PostgreSql;
using Vintello.Common.Repositories;

namespace Vintello.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repo;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<RetrievedCategoryDto?> CreateAsync(CreatedCategoryDto categoryDto)
    {
        Category? category = await _repo.CreateAsync(_mapper.Map<Category>(categoryDto));
        if (category is not null) return _mapper.Map<RetrievedCategoryDto>(category);
        return null;
    }

    public async Task<IEnumerable<RetrievedCategoriesDto>> RetrieveAsync()
    {
        var categories = await _repo.RetrieveAllAsync();
        return _mapper.Map<IEnumerable<RetrievedCategoriesDto>>(categories);
    }

    public async Task<RetrievedCategoryDto?> RetrieveByIdAsync(int id)
    {
        Category? category = await _repo.RetrieveByIdAsync(id);
        if (category is null) return null;
        return _mapper.Map<RetrievedCategoryDto>(category);
    }

    public async Task<bool?> UpdateAsync(int id, UpdatedCategoryDto category)
    {
        Category? existing = await _repo.RetrieveByIdAsync(id);
        if (existing is null) return null;

        Category updatedCategory = _mapper.Map(category, existing);

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