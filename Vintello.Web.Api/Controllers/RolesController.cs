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
    public async Task<IActionResult> CreateRole([FromBody] CreatedRolesDto role)
    {
        if (!ModelState.IsValid) return BadRequest();
        RetrivedRoleDto? createdRole = await _service.CreateAsync(role);
        if (createdRole is null) return BadRequest();
        return CreatedAtRoute(nameof(RetrieveRole),
            new { id = createdRole.Id },
            createdRole);
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<RetrivedRolesDto>))]
    public async Task<IEnumerable<RetrivedRolesDto>> RetrieveRoles()
    {
        return await _service.RetrieveAsync();
    }

    [HttpGet("{id}", Name = nameof(RetrieveRole))]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> RetrieveRole(int id)
    {
        RetrivedRoleDto? role = await _service.RetrieveByIdAsync(id);
        if (role is null) return NotFound();
        return Ok(role);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UpdateRole(int id, [FromBody] UpdatedRoleDto role)
    {
        if (!ModelState.IsValid) return BadRequest();
        bool? updated = await _service.UpdateAsync(id, role);
        if (updated == true) return NoContent();
        if (updated == false) return BadRequest();
        return NotFound();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> DeleteRole(int id)
    {
        bool? deleted = await _service.DeleteAsync(id);
        if (deleted == true) return NoContent();
        if (deleted == null) return NotFound();
        return BadRequest();
    }
}