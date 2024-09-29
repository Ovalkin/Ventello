using AutoMapper;
using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;
using Vintello.Common.Repositories;

namespace Vintello.Services;

public class ItemService : IItemService
{
    private readonly IItemRepository _repo;
    private readonly IMapper _mapper;

    public ItemService(IItemRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    
    public async Task<RetrievedItemDto?> CreateAsync(CreatedItemDto item)
    {
        Item? addItem = _mapper.Map<Item>(item);
        addItem.Status = "created";
        addItem.CreatedAt = DateTime.UtcNow;
        Item? addedItem = await _repo.CreateAsync(addItem);
        return _mapper.Map<RetrievedItemDto?>(addedItem);
    }

    public async Task<IEnumerable<RetrievedItemDto>> RetrieveAsync(string? status, int? user, int? category)
    {
        IEnumerable<RetrievedItemDto> allItems = _mapper.Map<IEnumerable<RetrievedItemDto>>((await _repo.RetrieveAllAsync()).ToList()); 
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
        return _mapper.Map<RetrievedItemDto?>(await _repo.RetrieveByIdAsync(id));
    }

    public async Task<bool?> UpdateAsync(int id, UpdatedItemDto item)
    {
        Item? existing = await _repo.RetrieveByIdAsync(id);
        if (existing is null) return null;

        Item updatedItem = _mapper.Map(item, existing);
        
        updatedItem.UpdatedAt = DateTime.UtcNow;
        
        Item? updated = await _repo.UpdateAsync(id, updatedItem);
        if (updated is null) return false;
        else return true;
    }

    public async Task<bool?> DeleteAsync(int id)
    {
        Item? item = await _repo.RetrieveByIdAsync(id);
        if (item is null) return null;
        return await _repo.DeleteAsync(item);
    }
}