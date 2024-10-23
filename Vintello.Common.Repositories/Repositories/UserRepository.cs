using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.Repositories;

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
        await _db.Users.AddAsync(user);
        int affected = await _db.SaveChangesAsync();
        if (affected == 1)
        {
            if (_userCache is null) return user;
            return _userCache.AddOrUpdate(user.Id, user, UpdateCache);
        }
        return null;
    }

    public async Task<User?> RetrieveByIdAsync(int id)
    {
        if (_userCache is null) return null!;
        _userCache.TryGetValue(id, out User? user);
        if (user is null) return null;
        await _db.Entry(user)
            .Collection(u => u.Items)
            .Query()
            .LoadAsync();
        return UpdateCache(id, user);
    }

    public Task<IEnumerable<User>> RetrieveAllAsync()
    {
        return Task.FromResult(_userCache?.Values ?? Enumerable.Empty<User>());
    }

    public async Task<bool?> UpdateAsync(int id, User newUser)
    {
        _db.Update(newUser);
        int affected = await _db.SaveChangesAsync();
        if (affected == 1) return true;
        return false;
    }

    public async Task<bool> DeleteAsync(User user)
    {
        _db.Users.Remove(user);
        int affected = await _db.SaveChangesAsync();
        if (affected == 1)
            if (_userCache is not null) return _userCache.TryRemove(user.Id, out user!);
        return false;
    }
}