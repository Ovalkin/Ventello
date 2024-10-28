using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Repositories;

public interface ICategoryRepository
{
    Task<Category?> CreateAsync(Category category);
    Task<Category?> RetrieveByIdAsync(int id);
    Task<IEnumerable<Category>> RetrieveAllAsync();
    Task<Category?> UpdateAsync(int id, Category category);
    Task<bool> DeleteAsync(Category category);
}