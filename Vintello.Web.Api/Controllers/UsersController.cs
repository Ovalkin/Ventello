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
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] CreateUserDto? user)
    {
        if (user is null) return BadRequest();
        RetrivedUserDto? addedUser = await _service.CreateAsync(user);
        if (addedUser is null) return BadRequest();
        else return Ok(addedUser);
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<RetrivedUsersDto>))]
    public async Task<IActionResult> GetUsers(string? location)
    {
        return Ok(await _service.RetriveAllOrLocationUserAsync(location));
    }
    
    [HttpGet("{id}", Name = nameof(GetUser))]
    [ProducesResponseType(200, Type = typeof(RetrivedUserDto))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetUser(int id)
    {
        RetrivedUserDto? user = await _service.RetriveByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(404)]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UpdateUser(int id,[FromBody] UpdatedUserDto? newUser)
    {
        if (newUser is null) return BadRequest();
        RetrivedUserDto? updatedUser = await _service.UpdateAsync(id, newUser);
        if (updatedUser == null) return NotFound();
        else return Ok(updatedUser);
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(404)]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> DeleteUser(int id)
    {
        bool? deleted = await _service.DeleteAsync(id);
        if (deleted == null) return NotFound();
        else if (deleted == true) return NoContent();
        else return BadRequest();
    }
}