using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;
using Vintello.Services;

namespace Vintello.Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IItemService _service;

    public ItemsController(IItemService service)
    {
        _service = service;
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(RetrievedItemDto))]
    [ProducesResponseType(400)]
    [Authorize]
    public async Task<IActionResult> CreateItem([FromBody] CreatedItemDto item)
    {
        if (!ModelState.IsValid) return BadRequest();
        RetrievedItemDto? createdItem = await _service.CreateAsync(item);
        if (createdItem is null) return BadRequest();
        return CreatedAtRoute(nameof(RetrieveItem), new { id = createdItem.Id },
            createdItem);
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<RetrievedItemDto>))]
    public async Task<IEnumerable<RetrievedItemDto>> RetrieveItems(string? status, int? user, int? category)
    {
        return await _service.RetrieveAsync(status, user, category);
    }

    [HttpGet("{id}", Name = nameof(RetrieveItem))]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> RetrieveItem(int id)
    {
        RetrievedItemDto? item = await _service.RetrieveByIdAsync(id);
        if (item is null) return NotFound();
        return Ok(item);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [Authorize]
    public async Task<IActionResult> UpdateItem(int id, UpdatedItemDto item)
    {
        if (!ModelState.IsValid) return BadRequest();
        bool? updated = await _service.UpdateAsync(id, item);
        if (updated == true) return NoContent();
        if (updated == false) return BadRequest();
        return NotFound();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [Authorize]
    public async Task<IActionResult> DeleteItem(int id)
    {
        bool? deleted = await _service.DeleteAsync(id);
        if (deleted == true) return NoContent();
        if (deleted == null) return NotFound();
        return BadRequest();
    }
}