using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;
using Vintello.Common.Repositories;

namespace Vintello.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _repo;

    public RoleService(IRoleRepository repo)
    {
        _repo = repo;
    }

    public async Task<RetrievedRoleDto?> CreateAsync(CreatedRoleDto createdRole)
    {
        Role? retrieveRole = await _repo.CreateAsync(CreatedRoleDto.CreateRole(createdRole));
        if (retrieveRole is null) return null;
        return RetrievedRoleDto.CreateDto(retrieveRole);
    }

    public async Task<RetrievedRoleDto?> RetrieveByIdAsync(int id)
    {
        Role? role = await _repo.RetrieveByIdAsync(id);
        if (role is null) return null;
        return RetrievedRoleDto.CreateDto(role);
    }

    public async Task<IEnumerable<RetrievedRolesDto>> RetrieveAsync()
    {
        var roles = await _repo.RetrieveAllAsync();
        return RetrievedRolesDto.CreateDto(roles);
    }

    public async Task<bool?> UpdateAsync(int id, UpdatedRoleDto role)
    {
        Role? existing = await _repo.RetrieveByIdAsync(id);
        if (existing is null) return null;

        Role updatedRole = UpdatedRoleDto.CreateRole(role, existing);
        
        Role? updated = await _repo.UpdateAsync(id, updatedRole);
        if (updated is null) return false;
        return true;
    }

    public async Task<bool?> DeleteAsync(int id)
    {
        Role? role = await _repo.RetrieveByIdAsync(id);
        if (role is null) return null;
        return await _repo.DeleteAsync(role);
    }
}