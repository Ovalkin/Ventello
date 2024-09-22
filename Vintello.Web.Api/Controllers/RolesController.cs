using Microsoft.AspNetCore.Mvc;
using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;
using Vintello.Common.Repositories;
using Vintello.Services;

namespace Vintello.Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly IRoleService _service;
    private readonly IRoleRepository _repo;

    public RolesController(IRoleService service, IRoleRepository repo)
    {
        _service = service;
        _repo = repo;
    }
    
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] CreatedUpdatedRetrivedRolesDto? createdRole)
    {
        if (createdRole is null) return BadRequest();
        RetriveRoleDto? addedRole = await _service.CreateAsync(createdRole);
        if (addedRole is null) return BadRequest();
        else return Ok(addedRole);
    }
    
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
    
    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IEnumerable<Role>> GetAll()
    {
        return await _repo.RetriveAllAsync();
    }
    
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