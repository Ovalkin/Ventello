using Microsoft.AspNetCore.Mvc;
using Vintello.Common.DTOs;
using Vintello.Services;

namespace Vintello.Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(ICategoryService service) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(RetrivedCategoryDto))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateCategory([FromBody] CreatedCategoryDto? category)
    {
        if (category is null) return BadRequest();
        RetrivedCategoryDto? createdCategory = await service.CreateAsync(category);
        if (createdCategory is null) return BadRequest();
        return CreatedAtRoute(nameof(GetCategory),
            new { id = createdCategory.Id },
            createdCategory);
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<RetrivedCategoriesDto>))]
    public async Task<IEnumerable<RetrivedCategoriesDto>> RetrieveCategories()
    {
        return await service.RetrieveAsync();
    }

    [HttpGet("{id}", Name = nameof(GetCategory))]
    [ProducesResponseType(200, Type = typeof(RetrivedCategoryDto))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetCategory(int id)
    {
        RetrivedCategoryDto? category = await service.RetrieveByIdAsync(id);
        if (category is null) return NotFound();
        return Ok(category);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdatedCategoryDto? category)
    {
        if (category is null) return BadRequest();
        bool? updated = await service.UpdateAsync(id, category);
        if (updated == true) return NoContent();
        else if (updated == false) return BadRequest();
        else return NotFound();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        bool? deleted = await service.DeleteAsync(id);
        if (deleted == true) return NoContent();
        else if (deleted == null) return NotFound();
        else return BadRequest();
    }
}