using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.Repositories;

public interface ICategoryRepository
{
    Task<Category?> CreateAsync(Category? category);
    Task<Category?> RetriveByIdAsync(int id);
    Task<IEnumerable<Category>> RetriveAllAsync();
    Task<Category?> UpdateAsync(int id, Category category);
    Task<bool> DeleteAsync(Category category);
}