using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.Repositories;

public interface IItemRepository
{
    Task<Item?> CreateAsync(Item item);
    Task<Item?> RetrieveByIdAsync(int id);
    Task<IEnumerable<Item>> RetrieveAllAsync();
    Task<Item?> UpdateAsync(int id, Item item);
    Task<bool> DeleteAsync(Item item);

}