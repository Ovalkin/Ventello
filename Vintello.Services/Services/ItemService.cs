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
    
    public async Task<CreatedItemDto?> CreateAsync(CreatedItemDto item)
    {
        Item? addItem = _mapper.Map<Item>(item);
        addItem.Status = "created";
        addItem.CreatedAt = DateTime.UtcNow;
        Item? addedItem = await _repo.CreateAsync(addItem);
        return _mapper.Map<CreatedItemDto?>(addedItem);
    }

    public async Task<IEnumerable<RetrivedItemsDto>> RetriveByParamsAsync(string? status, int? user, int? category)
    {
        IEnumerable<RetrivedItemsDto> allItems = _mapper.Map<IEnumerable<RetrivedItemsDto>>((await _repo.RetriveAllAsync()).ToList()); 
        if (string.IsNullOrWhiteSpace(status) && user is null && category is null) return allItems;
        var retrivedItemsDtos = allItems as RetrivedItemsDto[] ?? allItems.ToArray();
        IEnumerable<RetrivedItemsDto> items = retrivedItemsDtos;
        if(!string.IsNullOrWhiteSpace(status)) 
            items = items.Intersect(retrivedItemsDtos.Where(i => i.Status == status));
        if (user is not null)
            items = items.Intersect(retrivedItemsDtos.Where(i => i.UserId == user));
        if (category is not null)
            items = items.Intersect(retrivedItemsDtos.Where(i => i.CategoryId == category));
        return items;
    }

    public async Task<RetrivedItemsDto?> RetriveByIdAsync(int id)
    {
        return _mapper.Map<RetrivedItemsDto?>(await _repo.RetriveByIdAsync(id));
    }

    public async Task<RetrivedItemsDto?> UpdateAsync(int id, UpdatedItemDto updatedItemDto)
    {
        Item item = _mapper.Map<Item>(updatedItemDto);
        Item? findItem = await _repo.RetriveByIdAsync(id);
        if (findItem is null) return null;
        item.Id = findItem.Id;
        item.UserId = findItem.UserId;
        item.UpdatedAt = DateTime.UtcNow;
        return _mapper.Map<RetrivedItemsDto?>(await _repo.UpdateAsync(id, item));
    }

    public async Task<bool?> DeleteAsync(int id)
    {
        Item? item = await _repo.RetriveByIdAsync(id);
        if (item is null) return null;
        return await _repo.RemoveAsync(id);
    }
}