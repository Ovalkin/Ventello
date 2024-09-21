using Microsoft.AspNetCore.Mvc;
using Vintello.Common.EntityModel.PostgreSql;
using Vintello.Common.Repositories;

namespace Vintello.Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IItemRepository _repo;

    public ItemsController(IItemRepository repo)
    {
        _repo = repo;
    }
    
    //POST: api/items
    //BODY: Item (JSON)
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] Item? item)
    {
        if (item is null) return BadRequest();
        item.Status = item.Status.ToLower();
        Item? addedItem = await _repo.CreateAsync(item);
        if (addedItem is null) return BadRequest();
        else return Ok(addedItem);
    }
    
    //GET: api/items
    //GET: api/items?status=[status]&user=[userId]&category=[status]
    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IEnumerable<Item>> GetItems(string? status, int? user, int? category)
    {
        IEnumerable<Item> allItems = (await _repo.RetriveAllAsync()).ToList();
        if (string.IsNullOrWhiteSpace(status) && user is null && category is null) return allItems;
        IEnumerable<Item> items = allItems;
        if(!string.IsNullOrWhiteSpace(status)) 
            items = items.Intersect(allItems.Where(i => i.Status == status));
        if (user is not null)
            items = items.Intersect(allItems.Where(i => i.UserId == user));
        if (category is not null)
            items = items.Intersect(allItems.Where(i => i.CategoryId == category));
        return items;
    }
    
    //GET: api/items/[id]
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetItem(int id)
    {
        Item? item = await _repo.RetriveByIdAsync(id);
        if (item is not null) return Ok(item);
        else return NotFound();
    }
    
    //PUT: api/items/[id]
    //BODY: Item (JSON)
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UpdateItem(int id, Item? item)
    {
        if (item is null) return BadRequest();
        item.Status = item.Status.ToLower();
        Item? newItem = await _repo.UpdateAsync(id, item);
        if (newItem is null) return NotFound();
        else return Ok(newItem);
    }
    
    //DELETE: api/items/[id]
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> RemoveItem(int id)
    {
        Item? item = await _repo.RetriveByIdAsync(id);
        if (item is null) return NotFound();
        bool deleted = await _repo.RemoveAsync(id);
        if (deleted) return NoContent();
        else return BadRequest();
    }
}