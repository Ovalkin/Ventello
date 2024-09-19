using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Web.Api.Repositories;

public class UserRepository : IUserRepository
{
    private static ConcurrentDictionary<int, User>? _userCache;
    private readonly VintelloContext _db;
    public UserRepository(VintelloContext db)
    {
        _db = db;
        _userCache ??= new ConcurrentDictionary<int, User>
            (db.Users.ToDictionary(u => u.Id));
    }

    private User UpdateCache(int id, User user)
    {
        if (_userCache is not null)
        {
            if (_userCache.TryGetValue(id, out User? old))
            {
                if (_userCache.TryUpdate(id, user, old)) return user;
            }
        }

        return null!;
    }

    public async Task<User?> CreateAsync(User user)
    {
        user.RoleNavigation = await _db.Roles.FindAsync(user.Role);
        
        user.Location = user.Location?.ToLower();
        await _db.Users.AddAsync(user);
        int affected = await _db.SaveChangesAsync();
        if (affected == 1)
        {
            if (_userCache is null) return user;
            else return _userCache.AddOrUpdate(user.Id, user, UpdateCache);
        }
        else return null;
    }

    public Task<User?> RetrieveByIdAsync(int id)
    {
        if (_userCache is null) return null!;
        _userCache.TryGetValue(id, out User? user);
        return Task.FromResult(user);
    }

    public Task<IEnumerable<User>> RetrieveAllAsync()
    {
        return Task.FromResult(_userCache?.Values ?? Enumerable.Empty<User>());
    }

    public async Task<User?> UpdateAsync(int id, User newUser)
    {
        newUser.Location = newUser.Location?.ToLower();
        _db.Update(newUser);
        int affected = await _db.SaveChangesAsync();
        if (affected == 1) return UpdateCache(id, newUser);
        else return null;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        User? user = await _db.Users.FindAsync(id);
        if (user is null) return false;
        _db.Users.Remove(user);
        int affected = await _db.SaveChangesAsync();
        if (affected == 1)
            if (_userCache is not null) return _userCache.TryRemove(id, out user);
        return false;
    }
}