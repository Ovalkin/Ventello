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
    public async Task<IActionResult> CreateRole([FromBody] CreatedRolesDto? role)
    {
        if (role is null) return BadRequest();
        RetrivedRoleDto? createdRole = await _service.CreateAsync(role);
        if (createdRole is null) return BadRequest();
        return CreatedAtRoute(nameof(RetriveRole),
            new {id = createdRole.Id},
            createdRole);
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<RetrivedRolesDto>))]
    public async Task<IEnumerable<RetrivedRolesDto>> RetriveRoles()
    {
        return await _service.RetriveAsync();
    }
    
    [HttpGet("{id}", Name = nameof(RetriveRole))]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> RetriveRole(int id)
    {
        RetrivedRoleDto? role = await _service.RetriveByIdAsync(id);
        if (role is null) return NotFound();
        return Ok(role);
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UpdateRole(int id, [FromBody] UpdatedRoleDto? role)
    {
        if (role is null) return BadRequest();
        bool? updated = await _service.UpdateAsync(id, role);
        if (updated == true) return NoContent();
        else if (updated == false) return BadRequest();
        else return NotFound();
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> DeleteRole(int id)
    {
        bool? deleted = await _service.DeleteAsync(id);
        if (deleted == true) return NoContent();
        else if (deleted == null) return NotFound();
        else return BadRequest();
    }
}