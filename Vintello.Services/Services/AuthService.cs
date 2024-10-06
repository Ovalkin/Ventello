using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;
using Vintello.Common.Repositories;

namespace Vintello.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _repo;
    public AuthService(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task<User?> Auth(string email, string password)
    {
        IEnumerable<User> users = await _repo.RetrieveAllAsync();
        User? user = users.FirstOrDefault(u => u.Email == email);
        if (user is null) return null;
        if (user.Password != password) return null;
        return await _repo.RetrieveByIdAsync(user.Id);
    }
}