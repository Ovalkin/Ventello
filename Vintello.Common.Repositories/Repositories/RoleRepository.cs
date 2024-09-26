using System.Collections.Concurrent;
using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.Repositories;

public class RoleRepository : IRoleRepository
{
    private static ConcurrentDictionary<int, Role>? _roleCashe;
    private readonly VintelloContext _db;

    public RoleRepository(VintelloContext db)
    {
        _db = db;
        _roleCashe ??= new ConcurrentDictionary<int, Role>
            (db.Roles.ToDictionary(r => r.Id));
    }

    private Role UpdateCache(int id, Role role)
    {
        if (_roleCashe is not null)
        {
            if (_roleCashe.TryGetValue(id, out Role? old))
            {
                if (_roleCashe.TryUpdate(id, role, old)) return role;
            }
        }
        return null!;
    }
    
    public async Task<Role?> CreateAsync(Role role)
    {
        await _db.Roles.AddAsync(role);
        int affected = await _db.SaveChangesAsync();
        if (affected == 1)
        {
            if (_roleCashe is null) return role;
            else return _roleCashe.AddOrUpdate(role.Id, role, UpdateCache);
        }
        else return null;
    }

    public async Task<Role?> RetrieveByIdAsync(int id)
    {
        if (_roleCashe is null) return null!;
        _roleCashe.TryGetValue(id, out Role? role);
        if (role is null) return null;
        await _db.Entry(role).Collection(r => r.Users).LoadAsync();
        return role;
    }

    public Task<IEnumerable<Role>> RetrieveAllAsync()
    {
        return Task.FromResult(_roleCashe?.Values ?? Enumerable.Empty<Role>());
    }

    public async Task<Role?> UpdateAsync(int id, Role newRole)
    {
        _db.Update(newRole);
        int affected = await _db.SaveChangesAsync();
        if (affected == 1) return UpdateCache(id, newRole);
        else return null;
    }

    public async Task<bool> DeleteAsync(Role role)
    {
        _db.Roles.Remove(role);
        int affected = await _db.SaveChangesAsync();
        if (affected == 1)
            if (_roleCashe is not null) return _roleCashe.TryRemove(role.Id, out role!);
        return false;
    }
}