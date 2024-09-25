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

    public async Task<RetrivedRoleDto?> CreateAsync(CreatedUpdatedRetrivedRolesDto createdRole)
    {
        createdRole.RoleName = createdRole.RoleName.ToLower();
        Role? retriveRole = await _repo.CreateAsync(_mapper.Map<Role>(createdRole));
        if (retriveRole is null) return null;
        else return _mapper.Map<RetrivedRoleDto>(retriveRole);
    }

    public async Task<RetrivedRoleDto?> RetriveByNameAsync(string name)
    {
        name = name.ToLower();
        Role? role = await _repo.RetriveByNameAsync(name);
        if (role is null) return null;
        return _mapper.Map<RetrivedRoleDto?>(role);
    }

    public async Task<IEnumerable<CreatedUpdatedRetrivedRolesDto>> RetriveAllAsync()
    {
        var roles = await _repo.RetriveAllAsync();
        return _mapper.Map<IEnumerable<CreatedUpdatedRetrivedRolesDto>>(roles);
    }

    public async Task<bool?> UpdateAsync(string name, CreatedUpdatedRetrivedRolesDto role)
    {
        name = name.ToLower();
        Role? existing = await _repo.RetriveByNameAsync(name);
        if (existing is null) return null;
        
        Role updatedRole =_mapper.Map<Role>(role);
        
        Role? updated = await _repo.UpdateAsync(name, updatedRole);
        if (updated is null) return false;
        else return true;
    }

    public async Task<bool?> DeleteAsync(string name)
    {
        name = name.ToLower();
        Role? role = await _repo.RetriveByNameAsync(name);
        if (role is null) return null;
        return await _repo.DeleteAsync(name);
    }
}