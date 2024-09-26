using Vintello.Common.DTOs;

namespace Vintello.Services;

public interface IRoleService
{
    Task<RetrivedRoleDto?> CreateAsync(CreatedRolesDto createdRole);
    Task<RetrivedRoleDto?> RetriveByIdAsync(int id);
    Task<IEnumerable<RetrivedRolesDto>> RetriveAsync();
    Task<bool?> UpdateAsync(int id, UpdatedRoleDto role);
    Task<bool?> DeleteAsync(int id);
}