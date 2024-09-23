using Microsoft.AspNetCore.Mvc;
using Vintello.Common.DTOs;
using Vintello.Services;

namespace Vintello.Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly IRoleService _service;

    public RolesController(IRoleService service)
    {
        _service = service;
    }
    
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] CreatedUpdatedRetrivedRolesDto? createdRole)
    {
        if (createdRole is null) return BadRequest();
        RetrivedRoleDto? addedRole = await _service.CreateAsync(createdRole);
        if (addedRole is null) return BadRequest();
        else return Ok(addedRole);
    }
    
    [HttpGet("{name}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetByName(string name)
    {
        RetrivedRoleDto? role = await _service.RetriveByNameAsync(name);
        if (role is null) return NotFound();
        else return Ok(role);
    }
    
    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IEnumerable<CreatedUpdatedRetrivedRolesDto>> GetAll()
    {
        return await _service.RetriveAllAsync();
    }
    
    [HttpPut("{name}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UpdateRole(string name, [FromBody] CreatedUpdatedRetrivedRolesDto? role)
    {
        if (role is null || role.RoleName != name) return BadRequest();
        CreatedUpdatedRetrivedRolesDto? updatedRole = await _service.UpdateAsync(name, role);
        if (updatedRole is null) return NotFound();
        else return Ok(updatedRole);
    }
    
    [HttpDelete("{name}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> DeleteRole(string name)
    {
        bool? deleted = await _service.DeleteAsync(name);
        if (deleted == true) return NoContent();
        else if (deleted == false) return NotFound();
        return BadRequest();
    }
}