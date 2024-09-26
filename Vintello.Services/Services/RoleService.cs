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

    public async Task<RetrivedRoleDto?> CreateAsync(CreatedRolesDto createdRole)
    {
        Role? retriveRole = await _repo.CreateAsync(_mapper.Map<Role>(createdRole));
        if (retriveRole is null) return null;
        else return _mapper.Map<RetrivedRoleDto>(retriveRole);
    }

    public async Task<RetrivedRoleDto?> RetriveByIdAsync(int id)
    {
        Role? role = await _repo.RetriveByIdAsync(id);
        if (role is null) return null;
        return _mapper.Map<RetrivedRoleDto?>(role);
    }

    public async Task<IEnumerable<RetrivedRolesDto>> RetriveAsync()
    {
        var roles = await _repo.RetriveAllAsync();
        return _mapper.Map<IEnumerable<RetrivedRolesDto>>(roles);
    }

    public async Task<bool?> UpdateAsync(int id, UpdatedRoleDto role)
    {
        Role? existing = await _repo.RetriveByIdAsync(id);
        if (existing is null) return null;

        Role? updatedRole = _mapper.Map(role, existing);
        
        Role? updated = await _repo.UpdateAsync(id, updatedRole);
        if (updated is null) return false;
        else return true;
    }

    public async Task<bool?> DeleteAsync(int id)
    {
        Role? role = await _repo.RetriveByIdAsync(id);
        if (role is null) return null;
        return await _repo.DeleteAsync(role);
    }
}