using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.Repositories;

public interface IRoleRepository
{
    public Task<Role?> CreateAsync(Role role);
    public Task<Role?> RetriveByNameAsync(string roleName);
    public Task<IEnumerable<Role>> RetriveAllAsync();
    public Task<Role?> UpdateAsync(string roleName, Role newRole);
    public Task<bool?> DeleteAsync(string roleName);
}