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
    [ProducesResponseType(201, Type = typeof(RetrivedUserDto))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateRole([FromBody] CreatedUpdatedRetrivedRolesDto? role)
    {
        if (role is null) return BadRequest();
        RetrivedRoleDto? createdRole = await _service.CreateAsync(role);
        if (createdRole is null) return BadRequest();
        return CreatedAtRoute(nameof(RetriveRole),
            new {name = role.RoleName},
            createdRole);
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<CreatedUpdatedRetrivedRolesDto>))]
    public async Task<IEnumerable<CreatedUpdatedRetrivedRolesDto>> RetriveRoles()
    {
        return await _service.RetriveAllAsync();
    }
    
    [HttpGet("{name}", Name = nameof(RetriveRole))]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> RetriveRole(string name)
    {
        RetrivedRoleDto? role = await _service.RetriveByNameAsync(name);
        if (role is null) return NotFound();
        return Ok(role);
    }
    
    [HttpPut("{name}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UpdateRole(string name, [FromBody] CreatedUpdatedRetrivedRolesDto? role)
    {
        if (role is null) return BadRequest();
        bool? updated = await _service.UpdateAsync(name, role);
        if (updated == true) return NoContent();
        else if (updated == false) return BadRequest();
        else return NotFound();
    }
    
    [HttpDelete("{name}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> DeleteRole(string name)
    {
        bool? deleted = await _service.DeleteAsync(name);
        if (deleted == true) return NoContent();
        else if (deleted == null) return NotFound();
        else return BadRequest();
    }
}