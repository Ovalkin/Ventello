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

    public async Task<RetriveRoleDto?> CreateAsync(CreatedUpdatedRetrivedRolesDto createdRole)
    {
        createdRole.RoleName = createdRole.RoleName.ToLower();
        Role? retriveRole = await _repo.CreateAsync(_mapper.Map<Role>(createdRole));
        if (retriveRole is null) return null;
        else return _mapper.Map<RetriveRoleDto>(retriveRole);
    }
}