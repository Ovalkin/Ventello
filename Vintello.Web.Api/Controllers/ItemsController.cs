using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vintello.Common.DTOs;
using Vintello.Services;

namespace Vintello.Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController(IItemService service) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(RetrievedItemDto))]
    [ProducesResponseType(400)]
    [Authorize]
    public async Task<IActionResult> CreateItem([FromBody] CreatedItemDto item)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var role = User.FindFirst(ClaimTypes.Role)!.Value;
        if (!ModelState.IsValid) return BadRequest();
        RetrievedItemDto? createdItem = await service.CreateAsync(item, userId, role);
        if (createdItem is null) return BadRequest();
        return CreatedAtRoute(
            nameof(RetrieveItem),
            new { id = createdItem.Id },
            createdItem);
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<RetrievedItemDto>))]
    public async Task<IEnumerable<RetrievedItemDto>> RetrieveItems(string? status, int? user, int? category)
    {
        return await service.RetrieveAsync(status, user, category);
    }

    [HttpGet("{id:int}", Name = nameof(RetrieveItem))]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> RetrieveItem(int id)
    {
        RetrievedItemDto? item = await service.RetrieveByIdAsync(id);
        if (item is null) return NotFound();
        return Ok(item);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [Authorize]
    public async Task<IActionResult> UpdateItem(int id, UpdatedItemDto item)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var role = User.FindFirst(ClaimTypes.Role)!.Value;
        if (!ModelState.IsValid) return BadRequest();
        var updated = await service.UpdateAsync(id, item, userId, role);
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
    [Authorize]
    public async Task<IActionResult> DeleteItem(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var role = User.FindFirst(ClaimTypes.Role)!.Value;
        bool? deleted = await service.DeleteAsync(id, userId, role);
        return deleted switch
        {
            true => NoContent(),
            null => NotFound(),
            _ => BadRequest()
        };
    }
}