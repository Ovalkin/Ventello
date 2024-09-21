using Vintello.Common.DTOs;
using AutoMapper;
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
    
    public async Task<RetriveCategoryDto?> RetriveByIdAsync(int id)
    {
        var category = await _repo.RetriveByIdAsync(id);
        return _mapper.Map<RetriveCategoryDto>(category);
    }
}