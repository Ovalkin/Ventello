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
    [ProducesResponseType(201, Type = typeof(RetrivedUserDto))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateUser([FromBody] CreatedUserDto? user)
    {
        if (user is null) return BadRequest();
        RetrivedUserDto? createdUser = await _service.CreateAsync(user);
        if (createdUser is null) return BadRequest();
        else return CreatedAtRoute(nameof(RetriveUser),
            new {id = createdUser.Id},
            createdUser);
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<RetrivedUsersDto>))]
    public async Task<IActionResult> RetriveUsers(string? location)
    {
        return Ok(await _service.RetriveAsync(location));
    }
    
    [HttpGet("{id}", Name = nameof(RetriveUser))]
    [ProducesResponseType(200, Type = typeof(RetrivedUserDto))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> RetriveUser(int id)
    {
        RetrivedUserDto? user = await _service.RetriveByIdAsync(id);
        if (user is null) return NotFound();
        return Ok(user);
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(404)]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UpdateUser(int id,[FromBody] UpdatedUserDto? user)
    {
        if (user is null) return BadRequest();
        bool? updated = await _service.UpdateAsync(id, user);
        if (updated == true) return NoContent();
        else if (updated == false) return BadRequest();
        else return NotFound();
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(404)]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> DeleteUser(int id)
    {
        bool? deleted = await _service.DeleteAsync(id);
        if (deleted == true) return NoContent();
        else if (deleted == null) return NotFound();
        else return BadRequest();
    }
}