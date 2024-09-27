using System.Collections.Concurrent;
using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private static ConcurrentDictionary<int, Category>? _categoryCache;
    private readonly VintelloContext _db;

    public CategoryRepository(VintelloContext db)
    {
        _db = db;
        _categoryCache ??= new ConcurrentDictionary<int, Category>
            (_db.Categories.ToDictionary(category => category.Id));
    }

    private Category UpdateCache(int id, Category category)
    {
        if (_categoryCache is not null)
        {
            if (_categoryCache.TryGetValue(id, out Category? oldCategory))
            {
                if (_categoryCache.TryUpdate(id, category, oldCategory)) return category;
            }
        }
        return null!;
    }

    public async Task<Category?> CreateAsync(Category category)
    {
        await _db.Categories.AddAsync(category);
        int affected = await _db.SaveChangesAsync();
        if (affected == 1)
        {
            if (_categoryCache is null) return category;
            return _categoryCache.AddOrUpdate(category.Id, category, UpdateCache);
        }
        return null;
    }

    public async Task<Category?> RetrieveByIdAsync(int id)
    {
        if (_categoryCache is null) return null!;
        _categoryCache.TryGetValue(id, out Category? category);
        if (category is null) return null!;

        await _db.Entry(category).Collection(c => c.Items).LoadAsync();
        return category;
    }

    public Task<IEnumerable<Category>> RetrieveAllAsync()
    {
        return Task.FromResult(_categoryCache?.Values ?? Enumerable.Empty<Category>());
    }

    public async Task<Category?> UpdateAsync(int id, Category category)
    {
        _db.Update(category);
        int affected = await _db.SaveChangesAsync();
        if (affected == 1) return UpdateCache(id, category);
        return null;
    }

    public async Task<bool> DeleteAsync(Category category)
    {
        _db.Categories.Remove(category);
        int affected = await _db.SaveChangesAsync();
        if (affected == 1)
            if (_categoryCache is not null)
                return _categoryCache.TryRemove(category.Id, out category!);
        return false;
    }
}