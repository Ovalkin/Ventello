using AutoMapper;
using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;
using Vintello.Common.Repositories;

namespace Vintello.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _repo;
    private readonly IMapper _mapper;

    public RoleService(IRoleRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<RetrievedRoleDto?> CreateAsync(CreatedRolesDto createdRole)
    {
        Role? retriveRole = await _repo.CreateAsync(_mapper.Map<Role>(createdRole));
        if (retriveRole is null) return null;
        else return _mapper.Map<RetrievedRoleDto>(retriveRole);
    }

    public async Task<RetrievedRoleDto?> RetrieveByIdAsync(int id)
    {
        Role? role = await _repo.RetrieveByIdAsync(id);
        if (role is null) return null;
        return _mapper.Map<RetrievedRoleDto?>(role);
    }

    public async Task<IEnumerable<RetrievedRolesDto>> RetrieveAsync()
    {
        var roles = await _repo.RetrieveAllAsync();
        return _mapper.Map<IEnumerable<RetrievedRolesDto>>(roles);
    }

    public async Task<bool?> UpdateAsync(int id, UpdatedRoleDto role)
    {
        Role? existing = await _repo.RetrieveByIdAsync(id);
        if (existing is null) return null;

        Role? updatedRole = _mapper.Map(role, existing);
        
        Role? updated = await _repo.UpdateAsync(id, updatedRole);
        if (updated is null) return false;
        else return true;
    }

    public async Task<bool?> DeleteAsync(int id)
    {
        Role? role = await _repo.RetrieveByIdAsync(id);
        if (role is null) return null;
        return await _repo.DeleteAsync(role);
    }
}