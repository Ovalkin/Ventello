using System.Collections.Concurrent;
using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Repositories;

public class ItemRepository : IItemRepository
{
    private static ConcurrentDictionary<int, Item>? _itemCache;
    private readonly VintelloContext _db;

    public ItemRepository(VintelloContext db)
    {
        _db = db;
        _itemCache ??= new ConcurrentDictionary<int, Item>
            (db.Items.ToDictionary(item => item.Id));
    }

    private Item UpdateCache(int id, Item item)
    {
        if (_itemCache is not null)
        {
            if (_itemCache.TryGetValue(id, out Item? oldItem))
            {
                if (_itemCache.TryUpdate(id, item, oldItem)) return item;
            }
        }

        return null!;
    }
    
    public async Task<Item?> CreateAsync(Item? item)
    {
        if (item is null) return null;
        Category? category = await _db.Categories.FindAsync(item.CategoryId);
        if (category is null) return null;
        item.Category = category;
        User? user = await _db.Users.FindAsync(item.UserId);
        if (user is null) return null;
        item.User = user;
        await _db.Items.AddAsync(item);
        int affected = await _db.SaveChangesAsync();
        if (affected == 1)
        {
            if (_itemCache is null) return item;
            return _itemCache.AddOrUpdate(item.Id, item, UpdateCache);
        }
        return null;
    }

    public Task<Item?> RetrieveByIdAsync(int id)
    {
        if (_itemCache is null) return null!;
        _itemCache.TryGetValue(id, out Item? item);
        return Task.FromResult(item);
    }

    public Task<IEnumerable<Item>> RetrieveAllAsync()
    {
        return Task.FromResult(_itemCache?.Values ?? Enumerable.Empty<Item>());
    }
    public async Task<Item?> UpdateAsync(int id, Item item)
    {
        _db.Update(item);
        int affected = await _db.SaveChangesAsync();
        if (affected == 1) return UpdateCache(id, item);
        return null;
    }

    public async Task<bool> DeleteAsync(Item item)
    {
        _db.Items.Remove(item);
        int affected = await _db.SaveChangesAsync();
        if (affected == 1)
            if (_itemCache is not null) return _itemCache.TryRemove(item.Id, out item!);
        return false;
    }
}