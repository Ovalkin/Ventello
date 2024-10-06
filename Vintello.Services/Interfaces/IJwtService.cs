using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Services;

public interface IJwtService
{
    string GenerateToken(User user);
}