using System.Collections.Concurrent;
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
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>?> RetrieveAllAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User?> UpdateAsync(int id, User user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}