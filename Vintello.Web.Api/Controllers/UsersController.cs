using Vintello.Common.EntityModel.PostgreSql;
using Vintello.Web.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Vintello.Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _repo;

    public UsersController(IUserRepository repo)
    {
        _repo = repo;
    }

    //POST: api/users
    //BODY: User (JSON, XML)
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] User? user)
    {
        if (user is null) return BadRequest();
        User? addedUser = await _repo.CreateAsync(user);
        if (addedUser is null) return BadRequest();
        else return Ok();
    }

    //GET: api/users
    //GET: api/users?location=[location]
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
    public async Task<IEnumerable<User>> GetUsers(string? location)
    {
        if (string.IsNullOrWhiteSpace(location)) return await _repo.RetrieveAllAsync();
        location = location.ToLower();
        return (await _repo.RetrieveAllAsync()).Where(user => user.Location == location);
    }
    
    //GET: /api/users/[id]
    [HttpGet("{id}", Name = nameof(GetUser))]
    [ProducesResponseType(200, Type = typeof(User))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetUser(int id)
    {
        User? user = await _repo.RetrieveByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }
    
    //PUT: /api/users/[id]
    //BODY: User (JSON)
    [HttpPut("{id}")]
    [ProducesResponseType(404)]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UpdateUser(int id,[FromBody] User? newUser)
    {
        if (newUser == null || newUser.Id != id) return 
            BadRequest("ID изменяемого пользователя не получен в теле запроса");
        newUser.Location = newUser.Location?.ToLower();
        User? updatedUser = await _repo.UpdateAsync(id, newUser);
        if (updatedUser == null) return NotFound();
        else return Ok(updatedUser);
    }
    
    //DELETE: /api/users/[id]
    [HttpDelete("{id}")]
    [ProducesResponseType(404)]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> DeleteUser(int id)
    {
        User? user = await _repo.RetrieveByIdAsync(id);
        if (user is null) return NotFound();

        bool deleted = await _repo.DeleteAsync(id);
        if (deleted) return NoContent();
        else return BadRequest();
    }
}