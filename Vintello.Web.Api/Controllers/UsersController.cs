using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vintello.Common.DTOs;
using Vintello.Services;

namespace Vintello.Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController(IUserService service) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(RetrievedUserDto))]
    [ProducesResponseType(400)]
    [AllowAnonymous]
    public async Task<IActionResult> CreateUser([FromBody] CreatedUserDto user)
    {
        if (User.Identity?.IsAuthenticated != null)
            if (User.Identity.IsAuthenticated && User.FindFirst(ClaimTypes.Role)!.Value == "Client")
                return Forbid();
        if (!ModelState.IsValid) return BadRequest();
        RetrievedUserDto? createdUser = await service.CreateAsync(user);
        if (createdUser is null) return BadRequest();
        return CreatedAtRoute(
            nameof(RetrieveUser),
            new { id = createdUser.Id },
            createdUser);
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<RetrievedUsersDto>))]
    [AllowAnonymous]
    public async Task<IActionResult> RetrieveUsers(string? location)
    {
        return Ok(await service.RetrieveAsync(location));
    }

    [HttpGet("{id:int}", Name = nameof(RetrieveUser))]
    [ProducesResponseType(200, Type = typeof(RetrievedUserDto))]
    [ProducesResponseType(404)]
    [Authorize]
    public async Task<IActionResult> RetrieveUser(int id)
    {
        RetrievedUserDto? user = await service.RetrieveByIdAsync(id);
        if (user is null) return NotFound();
        return Ok(user);
    }

    [HttpPut("{id:int}")]
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
        return updated switch
        {
            true => NoContent(),
            false => BadRequest(),
            _ => NotFound()
        };
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(404)]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [Authorize]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var role = User.FindFirst(ClaimTypes.Role)!.Value;
        bool? deleted = await service.DeleteAsync(id, role, userId);
        return deleted switch
        {
            true => NoContent(),
            null => NotFound(),
            _ => BadRequest()
        };
    }
}