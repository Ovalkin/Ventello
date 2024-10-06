using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _confJwt;
    public JwtService(IConfiguration conf)
    {
        _confJwt = conf.GetSection("Jwt");
    }
    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_confJwt["Key"]!);
        var tokenDescription = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.Name)
            }),
            Expires = DateTime.Now.AddMinutes(double.Parse(_confJwt["TokenLifetimeMinutes"]!)),
            Issuer = _confJwt["Issuer"],
            Audience = _confJwt["Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescription);
        return tokenHandler.WriteToken(token);
    }
}