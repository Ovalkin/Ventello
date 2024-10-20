using Microsoft.AspNetCore.Mvc;
using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;
using Vintello.Services;

namespace Vintello.Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService serviceAuth, IJwtService serviceJwt) : ControllerBase
{
    [HttpPost("login")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid) return BadRequest();
        User? user = await serviceAuth.Auth(loginDto.Email, loginDto.Password);
        if (user is null) return BadRequest("Логин или пароль не верны!");
        string token = serviceJwt.GenerateToken(user);
        return Ok(token);
    }
    
    
}