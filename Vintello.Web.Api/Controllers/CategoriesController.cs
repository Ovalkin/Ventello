using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vintello.Common.DTOs;
using Vintello.Services;

namespace Vintello.Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(ICategoryService service) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(RetrievedCategoryDto))]
    [ProducesResponseType(400)]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> CreateCategory([FromBody] CreatedCategoryDto category)
    {
        if (!ModelState.IsValid) return BadRequest();
        RetrievedCategoryDto? createdCategory = await service.CreateAsync(category);
        if (createdCategory is null) return BadRequest();
        return CreatedAtRoute(
            nameof(GetCategory),
            new { id = createdCategory.Id },
            createdCategory);
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<RetrievedCategoriesDto>))]
    public async Task<IEnumerable<RetrievedCategoriesDto>> RetrieveCategories()
    {
        return await service.RetrieveAsync();
    }

    [HttpGet("{id:int}", Name = nameof(GetCategory))]
    [ProducesResponseType(200, Type = typeof(RetrievedCategoryDto))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetCategory(int id)
    {
        RetrievedCategoryDto? category = await service.RetrieveByIdAsync(id);
        if (category is null) return NotFound();
        return Ok(category);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdatedCategoryDto category)
    {
        if (!ModelState.IsValid) return BadRequest();
        bool? updated = await service.UpdateAsync(id, category);
        return updated switch
        {
            true => NoContent(),
            false => BadRequest(),
            _ => NotFound()
        };
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        bool? deleted = await service.DeleteAsync(id);
        return deleted switch
        {
            true => NoContent(),
            null => NotFound(),
            _ => BadRequest()
        };
    }
}