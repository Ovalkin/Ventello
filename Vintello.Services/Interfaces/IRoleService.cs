using Vintello.Common.DTOs;

namespace Vintello.Services;

public interface IRoleService
{
    Task<RetrivedRoleDto?> CreateAsync(CreatedUpdatedRetrivedRolesDto createdRole);
    Task<RetrivedRoleDto?> RetriveByNameAsync(string name);
    Task<IEnumerable<CreatedUpdatedRetrivedRolesDto>> RetriveAllAsync();
    Task<bool?> UpdateAsync(string name, CreatedUpdatedRetrivedRolesDto role);
    Task<bool?> DeleteAsync(string name);
}