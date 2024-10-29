using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;
using Vintello.Repositories;

namespace Vintello.Services;

public class ItemService(IItemRepository repo) : IItemService
{
    public async Task<RetrievedItemDto?> CreateAsync(CreatedItemDto item)
    {
        Item? addedItem = await repo.CreateAsync(CreatedItemDto.CreateItem(item));
        return addedItem != null ? RetrievedItemDto.CreateDto(addedItem) : null;
    }

    public async Task<IEnumerable<RetrievedItemDto>> RetrieveAsync(string? status, int? user, int? category)
    {
        IEnumerable<RetrievedItemDto> allItems = RetrievedItemDto.CreateDto((await repo.RetrieveAllAsync()).ToList());
        if (string.IsNullOrWhiteSpace(status) && user is null && category is null) return allItems;
        var retrievedItemsDto = allItems as RetrievedItemDto[] ?? allItems.ToArray();
        IEnumerable<RetrievedItemDto> items = retrievedItemsDto;
        if (!string.IsNullOrWhiteSpace(status))
            items = items.Intersect(retrievedItemsDto.Where(i => i.Status == status));
        if (user is not null)
            items = items.Intersect(retrievedItemsDto.Where(i => i.UserId == user));
        if (category is not null)
            items = items.Intersect(retrievedItemsDto.Where(i => i.CategoryId == category));
        return items;
    }

    public async Task<RetrievedItemDto?> RetrieveByIdAsync(int id)
    {
        Item? item = await repo.RetrieveByIdAsync(id);
        return item is null ? null : RetrievedItemDto.CreateDto(item);
    }

    public async Task<bool?> UpdateAsync(int id, UpdatedItemDto item)
    {
        Item? existing = await repo.RetrieveByIdAsync(id);
        if (existing is null) return null;
        existing = UpdatedItemDto.CreateItem(item, existing);
        Item? updated = await repo.UpdateAsync(id, existing);
        return updated is not null;
    }

    public async Task<bool?> DeleteAsync(int id)
    {
        Item? item = await repo.RetrieveByIdAsync(id);
        if (item is null) return null;
        return await repo.DeleteAsync(item);
    }
}