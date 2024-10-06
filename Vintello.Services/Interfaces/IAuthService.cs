using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Services;

public interface IAuthService
{
    Task<User?> Auth(string email, string password);
}