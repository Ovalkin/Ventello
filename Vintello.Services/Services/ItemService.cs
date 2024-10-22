using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;
using Vintello.Common.Repositories;

namespace Vintello.Services;

public class ItemService(IItemRepository repo) : IItemService
{
    public async Task<RetrievedItemDto?> CreateAsync(CreatedItemDto item, string userId, string userRole)
    {
        if (userRole == "Client" && userId != item.UserId.ToString())
            return null;
        Item? addedItem = await repo.CreateAsync(CreatedItemDto.CreateItem(item));
        if (addedItem != null) return RetrievedItemDto.CreateDto(addedItem);
        return null;
    }

    public async Task<IEnumerable<RetrievedItemDto>> RetrieveAsync(string? status, int? user, int? category)
    {
        IEnumerable<RetrievedItemDto> allItems = RetrievedItemDto.CreateDto((await repo.RetrieveAllAsync()).ToList());
        if (string.IsNullOrWhiteSpace(status) && user is null && category is null) return allItems;
        var retrievedItemsDto = allItems as RetrievedItemDto[] ?? allItems.ToArray();
        IEnumerable<RetrievedItemDto> items = retrievedItemsDto;
        if(!string.IsNullOrWhiteSpace(status)) 
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
        if (item is null) return null;
        return RetrievedItemDto.CreateDto(item);
    }

    public async Task<bool?> UpdateAsync(int id, UpdatedItemDto item, string userId, string userRole)
    {
        if (userRole == "Client" & userId != item.UserId)
            return false;
        Item? existing = await repo.RetrieveByIdAsync(id);
        if (existing is null) return null;
        Item updatedItem = UpdatedItemDto.CreateItem(item, existing);
        Item? updated = await repo.UpdateAsync(id, updatedItem);
        if (updated is null) return false;
        return true;
    }

    public async Task<bool?> DeleteAsync(int id, string userId, string userRole)
    {
        Item? item = await repo.RetrieveByIdAsync(id);
        if (item is null) return null;
        if (userRole == "Client" && userId != item.UserId.ToString())
            return false;
        return await repo.DeleteAsync(item);
    }
}