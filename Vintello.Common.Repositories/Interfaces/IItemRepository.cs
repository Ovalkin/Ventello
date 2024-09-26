using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.Repositories;

public interface IItemRepository
{
    Task<Item?> CreateAsync(Item? item);
    Task<Item?> RetriveByIdAsync(int id);
    Task<IEnumerable<Item>> RetriveAllAsync();
    Task<Item?> UpdateAsync(int id, Item item);
    Task<bool> DeleteAsync(Item item);

}