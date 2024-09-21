using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.Repositories;

public interface IItemRepository
{
    Task<Item?> CreateAsync(Item? item);
    Task<IEnumerable<Item>> RetriveAllAsync();
    Task<Item?> RetriveByIdAsync(int id);
    Task<Item?> UpdateAsync(int id, Item item);
    Task<bool> RemoveAsync(int id);

}