using System.Collections.Concurrent;
using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private static ConcurrentDictionary<int, Category>? _categoryCashe;
    private readonly VintelloContext _db;

    public CategoryRepository(VintelloContext db)
    {
        _db = db;
        _categoryCashe ??= new ConcurrentDictionary<int, Category>
            (_db.Categories.ToDictionary(category => category.Id));
    }

    private Category? UpdateCache(int id, Category category)
    {
        if (_categoryCashe is not null)
        {
            if (_categoryCashe.TryGetValue(id, out Category? oldCategory))
            {
                if (_categoryCashe.TryUpdate(id, category, oldCategory)) return category;
            }
        }   
        return null!;
    }
    public async Task<Category?> CreateAsync(Category? category)
    {
        if (category is null) return null;
        category.Name = category.Name.ToLower();
        await _db.Categories.AddAsync(category);
        int affected = await _db.SaveChangesAsync();
        if (affected == 1)
        {
            if (_categoryCashe is null) return category;
            else return _categoryCashe.AddOrUpdate(category.Id, category, UpdateCache!);
        }
        else return null;
    }

    public async Task<Category?> RetriveByIdAsync(int id)
    {
        if (_categoryCashe is null) return null!;
        _categoryCashe.TryGetValue(id, out Category? category);
        if (category is null) return null!; 
        
        await _db.Entry(category).Collection(c => c.Items).LoadAsync();
        return category;
    }

    public Task<IEnumerable<Category>> RetriveAllAsync()
    {
        return Task.FromResult(_categoryCashe?.Values ?? Enumerable.Empty<Category>());
    }

    public async Task<Category?> UpdateAsync(int id, Category category)
    {
        _db.Update(category);
        int affected = await _db.SaveChangesAsync();
        if (affected == 1) return UpdateCache(id, category);
        else return null;
    }

    public async Task<bool> DeleteAsync(Category category)
    {
        _db.Categories.Remove(category);
        int affected = await _db.SaveChangesAsync();
        if (affected == 1)
            if (_categoryCashe is not null)
                return _categoryCashe.TryRemove(category.Id, out category!);
        return false;
    }
}