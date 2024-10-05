using Vintello.Common.DTOs;

namespace Vintello.Services;

public interface IRoleService
{
    Task<RetrievedRoleDto?> CreateAsync(CreatedRoleDto role);
    Task<RetrievedRoleDto?> RetrieveByIdAsync(int id);
    Task<IEnumerable<RetrievedRolesDto>> RetrieveAsync();
    Task<bool?> UpdateAsync(int id, UpdatedRoleDto role);
    Task<bool?> DeleteAsync(int id);
}