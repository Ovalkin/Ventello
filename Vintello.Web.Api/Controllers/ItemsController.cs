using Microsoft.AspNetCore.Mvc;
using Vintello.Common.DTOs;
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
    [ProducesResponseType(201, Type = typeof(RetrivedItemDto))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateItem([FromBody] CreatedItemDto? item)
    {
        if (item is null) return BadRequest();
        RetrivedItemDto? createdItem = await _service.CreateAsync(item);
        if (createdItem is null) return BadRequest();
        return CreatedAtRoute(nameof(RetriveItem),
            new { id = createdItem.Id },
            createdItem);
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<RetrivedItemDto>))]
    public async Task<IEnumerable<RetrivedItemDto>> RetriveItems(string? status, int? user, int? category)
    {
        return await _service.RetriveByParamsAsync(status, user, category);
    }

    [HttpGet("{id}", Name = nameof(RetriveItem))]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> RetriveItem(int id)
    {
        RetrivedItemDto? item = await _service.RetriveByIdAsync(id);
        if (item is null) return NotFound();
        return Ok(item);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UpdateItem(int id, UpdatedItemDto? item)
    {
        if (item is null) return BadRequest();
        bool? updated = await _service.UpdateAsync(id, item);
        if (updated == true) return NoContent();
        else if (updated == false) return BadRequest();
        else return NotFound();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> RemoveItem(int id)
    {
        bool? deleted = await _service.DeleteAsync(id);
        if (deleted == true) return NoContent();
        else if (deleted == null) return NotFound();
        else return BadRequest();
    }
}