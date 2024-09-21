using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;
using Vintello.Common.Repositories;
using Vintello.Services;

namespace Vintello.Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICategoryService _service;

    public CategoriesController(ICategoryRepository repo, IMapper mapper, ICategoryService service)
    {
        _repo = repo;
        _mapper = mapper;
        _service = service;
    }
    
    //POST: api/categories
    //BODY: Category (JSON)
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto? createdCategory)
    {
        if (createdCategory is null) return BadRequest();
        var category = _mapper.Map<Category>(createdCategory);
        Category? addedCategory = await _repo.CreateAsync(category);
        if (addedCategory is null) return BadRequest();
        else return Ok(addedCategory);
    }
    
    //GET: api/categories
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
    public async Task<IEnumerable<Category>> GetCategories()
    {
        return await _repo.RetriveAllAsync();
    }
    
    //GET: api/categories/[id]
    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(RetriveCategoryDto))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetCategory(int id)
    {
        RetriveCategoryDto? categoryDto = await _service.RetriveByIdAsync(id);
        if (categoryDto is null) return NotFound();
        else return Ok(categoryDto);
    }
    
    //PUT: api/categories/[id]
    //BODY: Category (JSON)
    [HttpPut("{id}")]
    [ProducesResponseType(200, Type = typeof(Category))]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category? category)
    {
        if (category is null || id != category.Id) return BadRequest();
        category.Name = category.Name.ToLower();
        Category? updatedCategory = await _repo.UpdateAsync(id, category);
        if (updatedCategory is null) return NotFound();
        else return Ok(updatedCategory);
    }
    
    //DELETE: api/categories/[id]
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        Category? category = await _repo.RetriveByIdAsync(id);
        if (category is null) return NotFound();
        bool deleted = await _repo.DeleteAsync(id);
        if (deleted) return NoContent();
        else return BadRequest();
    }
}