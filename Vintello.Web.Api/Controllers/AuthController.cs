using Microsoft.AspNetCore.Mvc;
using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;
using Vintello.Services;

namespace Vintello.Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _serviceAuth;
    private readonly IJwtService _serviceJwt;
    public AuthController(IAuthService serviceAuth, IJwtService serviceJwt)
    {
        _serviceAuth = serviceAuth;
        _serviceJwt = serviceJwt;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid) return BadRequest();
        User? user = await _serviceAuth.Auth(loginDto.Email, loginDto.Password);
        if (user is null) return BadRequest();
        string token = _serviceJwt.GenerateToken(user);
        return Ok(token);
    }
}