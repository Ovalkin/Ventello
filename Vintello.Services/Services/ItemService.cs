using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;
using Vintello.Common.Repositories;

namespace Vintello.Services;

public class ItemService : IItemService
{
    private readonly IItemRepository _repo;

    public ItemService(IItemRepository repo)
    {
        _repo = repo;
    }
    
    public async Task<RetrievedItemDto?> CreateAsync(CreatedItemDto item)
    {
        Item? addedItem = await _repo.CreateAsync(CreatedItemDto.CreateItem(item));
        if (addedItem != null) return RetrievedItemDto.CreateDto(addedItem);
        return null;
    }

    public async Task<IEnumerable<RetrievedItemDto>> RetrieveAsync(string? status, int? user, int? category)
    {
        IEnumerable<RetrievedItemDto> allItems = RetrievedItemDto.CreateDto((await _repo.RetrieveAllAsync()).ToList());
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
        Item? item = await _repo.RetrieveByIdAsync(id);
        if (item is null) return null;
        return RetrievedItemDto.CreateDto(item);
    }

    public async Task<bool?> UpdateAsync(int id, UpdatedItemDto item)
    {
        Item? existing = await _repo.RetrieveByIdAsync(id);
        if (existing is null) return null;
        Item updatedItem = UpdatedItemDto.CreateItem(item, existing);
        Item? updated = await _repo.UpdateAsync(id, updatedItem);
        if (updated is null) return false;
        return true;
    }

    public async Task<bool?> DeleteAsync(int id)
    {
        Item? item = await _repo.RetrieveByIdAsync(id);
        if (item is null) return null;
        return await _repo.DeleteAsync(item);
    }
}