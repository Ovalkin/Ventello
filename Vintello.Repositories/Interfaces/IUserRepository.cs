using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Repositories;

public interface IUserRepository
{
    Task<User?> CreateAsync(User user);
    Task<User?> RetrieveByIdAsync(int id);
    Task<IEnumerable<User>> RetrieveAllAsync();
    Task<bool?> UpdateAsync(int id, User newUser);
    Task<bool> DeleteAsync(User user);
}