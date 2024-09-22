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
    public async Task<IActionResult> CreateCategory([FromBody] CreatedCategoryDto? createdCategory)
    {
        if (createdCategory is null) return BadRequest();
        RetrivedCategoryDto? addedCategory = await service.CreateAsync(createdCategory);
        if (addedCategory is null) return BadRequest();
        return CreatedAtRoute(
            routeName: nameof(GetCategory),
            routeValues: new { id = addedCategory.Id },
            value: addedCategory);
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<RetrivedCategoriesDto>))]
    public async Task<IEnumerable<RetrivedCategoriesDto>> GetCategories()
    {
        return await service.RetriveAllAsync();
    }

    [HttpGet("{id}", Name = nameof(GetCategory))]
    [ProducesResponseType(200, Type = typeof(RetrivedCategoryDto))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetCategory(int id)
    {
        RetrivedCategoryDto? categoryDto = await service.RetriveByIdAsync(id);
        if (categoryDto is null) return NotFound();
        else return Ok(categoryDto);
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