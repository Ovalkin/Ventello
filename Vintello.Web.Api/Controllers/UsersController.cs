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
    
    
    
    
    //POST: api/user
    //BODY: User (JSON, XML)
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] User? user)
    {
        if (user is null) return BadRequest();
        User? addedUser = await _repo.CreateAsync(user);
        if (addedUser is null) return BadRequest();
        else return Ok();
    }
}