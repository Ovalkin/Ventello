using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vintello.Common.DTOs;
using Vintello.Services;

namespace Vintello.Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
        _service = service;
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(RetrievedUserDto))]
    [ProducesResponseType(400)]
    
    public async Task<IActionResult> CreateUser([FromBody] CreatedUserDto user)
    {
        if (!ModelState.IsValid) return BadRequest();
        RetrievedUserDto? createdUser = await _service.CreateAsync(user);
        if (createdUser is null) return BadRequest();
        return CreatedAtRoute(nameof(RetrieveUser), new { id = createdUser.Id },
            createdUser);
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<RetrievedUsersDto>))]
    public async Task<IActionResult> RetrieveUsers(string? location)
    {
        return Ok(await _service.RetrieveAsync(location));
    }

    [HttpGet("{id}", Name = nameof(RetrieveUser))]
    [ProducesResponseType(200, Type = typeof(RetrievedUserDto))]
    [ProducesResponseType(404)]
    [Authorize]
    public async Task<IActionResult> RetrieveUser(int id)
    {
        RetrievedUserDto? user = await _service.RetrieveByIdAsync(id);
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
        if (!ModelState.IsValid) return BadRequest();
        bool? updated = await _service.UpdateAsync(id, user);
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
        bool? deleted = await _service.DeleteAsync(id);
        if (deleted == true) return NoContent();
        if (deleted == null) return NotFound();
        return BadRequest();
    }
}