using Microsoft.AspNetCore.Mvc;
using Vintello.Common.EntityModel.PostgreSql;
using Vintello.Common.Repositories;

namespace Vintello.Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly IRoleRepository _repo;

    public RolesController(IRoleRepository repo)
    {
        _repo = repo;
    }
    
    //POST: api/roles
    //BODY: Role (JSON)
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] Role? role)
    {
        if (role is null) return BadRequest();
        role.RoleName = role.RoleName.ToLower();
        Role? addedRole = await _repo.CreateAsync(role);
        if (addedRole is null) return BadRequest();
        else return Ok(role);
    }
    
    //GET: api/roles/[name]
    [HttpGet("{name}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetByName(string name)
    {
        name = name.ToLower();
        Role? role = await _repo.RetriveByNameAsync(name);
        if (role is null) return NotFound();
        else return Ok(role);
    }
    
    //GET: api/roles
    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IEnumerable<Role>> GetAll()
    {
        return await _repo.RetriveAllAsync();
    }
    
    //PUT: api/roles/[name]
    //BODY: Role (JSON)
    [HttpPut("{name}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UpdateRole(string name, [FromBody] Role? role)
    {
        if (role is null || role.RoleName != name) return BadRequest();
        name = name.ToLower();
        Role? updatedRole = await _repo.UpdateAsync(name, role);
        if (updatedRole is null) return NotFound();
        else return Ok(updatedRole);
    }
    
    //DELTE: api/roles/[name]
    [HttpDelete("{name}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> DeleteRole(string name)
    {
        name = name.ToLower();
        Role? role = await _repo.RetriveByNameAsync(name);
        if (role is null) return NotFound();
        bool deleted = await _repo.DeleteAsync(name);
        if (deleted) return NoContent();
        else return BadRequest();
    }
    
    
}