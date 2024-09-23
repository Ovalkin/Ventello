using Microsoft.AspNetCore.Mvc;
using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;
using Vintello.Common.Repositories;
using Vintello.Services;

namespace Vintello.Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IItemRepository _repo;
    private readonly IItemService _service;

    public ItemsController(IItemRepository repo, IItemService service)
    {
        _service = service;
        _repo = repo;
    }
    
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] CreatedItemDto? item)
    {
        if (item is null) return BadRequest();
        CreatedItemDto? addedItem = await _service.CreateAsync(item);
        if (addedItem is null) return BadRequest();
        else return Ok(addedItem);
    }
    
    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IEnumerable<RetrivedItemsDto>> GetItems(string? status, int? user, int? category)
    {
        return await _service.RetriveByParamsAsync(status, user, category);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetItem(int id)
    {
        RetrivedItemsDto? item = await _service.RetriveByIdAsync(id);
        if (item is not null) return Ok(item);
        else return NotFound();
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UpdateItem(int id, UpdatedItemDto? item)
    {
        if (item is null) return BadRequest();
        item.Status = item.Status.ToLower();
        RetrivedItemsDto? newItem = await _service.UpdateAsync(id, item);
        if (newItem is null) return NotFound();
        else return Ok(newItem);
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