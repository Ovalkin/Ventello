using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vintello.Common.DTOs;
using Vintello.Services;

namespace Vintello.Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService service) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(RetrievedUserDto))]
    [ProducesResponseType(400)]
    [AllowAnonymous]
    public async Task<IActionResult> CreateUser([FromBody] CreatedUserDto user)
    {
        if (!ModelState.IsValid) return BadRequest();
        RetrievedUserDto? createdUser = await service.CreateAsync(user);
        if (createdUser is null) return BadRequest();
        return CreatedAtRoute(nameof(RetrieveUser), new { id = createdUser.Id },
            createdUser);
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<RetrievedUsersDto>))]
    public async Task<IActionResult> RetrieveUsers(string? location)
    {
        return Ok(await service.RetrieveAsync(location));
    }

    [HttpGet("{id}", Name = nameof(RetrieveUser))]
    [ProducesResponseType(200, Type = typeof(RetrievedUserDto))]
    [ProducesResponseType(404)]
    [Authorize]
    public async Task<IActionResult> RetrieveUser(int id)
    {
        RetrievedUserDto? user = await service.RetrieveByIdAsync(id);
        if (user is null) return NotFound();
        return Ok(user);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(404)]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Authorize]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdatedUserDto user)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var role = User.FindFirst(ClaimTypes.Role)!.Value;
        
        if (!ModelState.IsValid) return BadRequest();
        bool? updated = await service.UpdateAsync(id, user, role, userId);
        if (updated == true) return NoContent();
        if (updated == false) return BadRequest();
        return NotFound();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(404)]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [Authorize]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var role = User.FindFirst(ClaimTypes.Role)!.Value;
        
        bool? deleted = await service.DeleteAsync(id, role, userId);
        if (deleted == true) return NoContent();
        if (deleted == null) return NotFound();
        return BadRequest();
    }
}