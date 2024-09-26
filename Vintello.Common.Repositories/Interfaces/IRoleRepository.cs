using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.Repositories;

public interface IRoleRepository
{
    public Task<Role?> CreateAsync(Role role);
    public Task<Role?> RetriveByIdAsync(int id);
    public Task<IEnumerable<Role>> RetriveAllAsync();
    public Task<Role?> UpdateAsync(int id, Role newRole);
    public Task<bool> DeleteAsync(Role role);
}