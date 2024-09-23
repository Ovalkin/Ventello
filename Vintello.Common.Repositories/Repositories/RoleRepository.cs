using System.Collections.Concurrent;
using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.Repositories;

public class RoleRepository : IRoleRepository
{
    private static ConcurrentDictionary<string, Role>? _roleCashe;
    private readonly VintelloContext _db;

    public RoleRepository(VintelloContext db)
    {
        _db = db;
        _roleCashe ??= new ConcurrentDictionary<string, Role>
            (db.Roles.ToDictionary(r => r.RoleName));
    }

    private Role UpdateCache(string roleName, Role role)
    {
        if (_roleCashe is not null)
        {
            if (_roleCashe.TryGetValue(roleName, out Role? old))
            {
                if (_roleCashe.TryUpdate(roleName, role, old)) return role;
            }
        }
        return null!;
    }
    
    public async Task<Role?> CreateAsync(Role role)
    {
        role.RoleName = role.RoleName.ToLower();
        await _db.Roles.AddAsync(role);
        int affected = await _db.SaveChangesAsync();
        if (affected == 1)
        {
            if (_roleCashe is null) return role;
            else return _roleCashe.AddOrUpdate(role.RoleName, role, UpdateCache);
        }
        else return null;
    }

    public async Task<Role?> RetriveByNameAsync(string roleName)
    {
        if (_roleCashe is null) return null!;
        roleName = roleName.ToLower();
        _roleCashe.TryGetValue(roleName, out Role? role);
        if (role is null) return null;
        await _db.Entry(role).Collection(r => r.Users).LoadAsync();
        return role;
    }

    public Task<IEnumerable<Role>> RetriveAllAsync()
    {
        return Task.FromResult(_roleCashe?.Values ?? Enumerable.Empty<Role>());
    }

    public async Task<Role?> UpdateAsync(string roleName, Role newRole)
    {
        roleName = roleName.ToLower();
        _db.Update(newRole);
        int affected = await _db.SaveChangesAsync();
        if (affected == 1) return UpdateCache(roleName, newRole);
        else return null;
    }

    public async Task<bool?> DeleteAsync(string roleName)
    {
        roleName = roleName.ToLower();
        Role? deletedRole = await _db.Roles.FindAsync(roleName);
        if (deletedRole is null) return null;
        _db.Roles.Remove(deletedRole);
        int affected = await _db.SaveChangesAsync();
        if (affected == 1)
            if (_roleCashe is not null)
                return _roleCashe.TryRemove(roleName, out deletedRole);
        return false;
    }
}