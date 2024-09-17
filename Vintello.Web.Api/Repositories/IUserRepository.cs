using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Web.Api.Repositories;

public interface IUserRepository
{
    Task<User?> CreateAsync(User user);
    Task<User?> RetrieveByIdAsync(int id);
    Task<IEnumerable<User>?> RetrieveAllAsync(int id);
    Task<User?> UpdateAsync(int id, User user);
    Task<bool> DeleteAsync(int id);
}