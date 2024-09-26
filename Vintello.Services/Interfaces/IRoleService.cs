using Vintello.Common.DTOs;

namespace Vintello.Services;

public interface IRoleService
{
    Task<RetrivedRoleDto?> CreateAsync(CreatedRolesDto role);
    Task<RetrivedRoleDto?> RetrieveByIdAsync(int id);
    Task<IEnumerable<RetrivedRolesDto>> RetrieveAsync();
    Task<bool?> UpdateAsync(int id, UpdatedRoleDto role);
    Task<bool?> DeleteAsync(int id);
}