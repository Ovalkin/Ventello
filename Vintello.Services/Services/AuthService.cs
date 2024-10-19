using Vintello.Common.EntityModel.PostgreSql;
using Vintello.Common.Repositories;

namespace Vintello.Services;

public class AuthService(IUserRepository repo) : IAuthService
{
    public async Task<User?> Auth(string email, string password)
    {
        IEnumerable<User> users = await repo.RetrieveAllAsync();
        User? user = users.FirstOrDefault(u => u.Email == email);
        if (user is null) return null;
        if (user.Password != password) return null;
        return await repo.RetrieveByIdAsync(user.Id);
    }
}