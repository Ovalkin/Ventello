using Vintello.Common.DTOs;

namespace Vintello.Services;

public interface IRoleService
{
    Task<RetriveRoleDto?> CreateAsync(CreatedUpdatedRetrivedRolesDto createdRole);
}