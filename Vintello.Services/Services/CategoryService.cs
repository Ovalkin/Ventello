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

    public async Task<RetrivedCategoryDto?> CreateAsync(CreatedUpdatedCategoryDto categoryDto)
    {
        Category? category = await _repo.CreateAsync(_mapper.Map<Category>(categoryDto));
        if (category is not null) return _mapper.Map<RetrivedCategoryDto>(category);
        else return null;
    }

    public async Task<IEnumerable<RetrivedCategoriesDto>> RetriveAllAsync()
    {
        var categories = await _repo.RetriveAllAsync();
        return _mapper.Map<IEnumerable<RetrivedCategoriesDto>>(categories);
    }
    
    public async Task<RetrivedCategoryDto?> RetriveByIdAsync(int id)
    {
        var category = await _repo.RetriveByIdAsync(id);
        return _mapper.Map<RetrivedCategoryDto>(category);
    }

    public async Task<bool?> UpdateAsync(int id, CreatedUpdatedCategoryDto category)
    {
        Category? existing = await _repo.RetriveByIdAsync(id);
        if (existing is null) return null;
        
        Category updatedCategory = _mapper.Map<Category>(category);
        updatedCategory.Id = id;
        
        Category? updated = await _repo.UpdateAsync(id, updatedCategory);
        if (updated is null) return false;
        else return true;
    }

    public async Task<bool?> DeleteAsync(int id)
    {
        Category? category = await _repo.RetriveByIdAsync(id);
        if (category is null) return null;
        return await _repo.DeleteAsync(category);
    }
}