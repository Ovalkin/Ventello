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
    
    public async Task<RetrivedItemDto?> CreateAsync(CreatedItemDto item)
    {
        Item? addItem = _mapper.Map<Item>(item);
        addItem.Status = "created";
        addItem.CreatedAt = DateTime.UtcNow;
        Item? addedItem = await _repo.CreateAsync(addItem);
        return _mapper.Map<RetrivedItemDto?>(addedItem);
    }

    public async Task<IEnumerable<RetrivedItemDto>> RetriveAsync(string? status, int? user, int? category)
    {
        IEnumerable<RetrivedItemDto> allItems = _mapper.Map<IEnumerable<RetrivedItemDto>>((await _repo.RetriveAllAsync()).ToList()); 
        if (string.IsNullOrWhiteSpace(status) && user is null && category is null) return allItems;
        var retrivedItemsDtos = allItems as RetrivedItemDto[] ?? allItems.ToArray();
        IEnumerable<RetrivedItemDto> items = retrivedItemsDtos;
        if(!string.IsNullOrWhiteSpace(status)) 
            items = items.Intersect(retrivedItemsDtos.Where(i => i.Status == status));
        if (user is not null)
            items = items.Intersect(retrivedItemsDtos.Where(i => i.UserId == user));
        if (category is not null)
            items = items.Intersect(retrivedItemsDtos.Where(i => i.CategoryId == category));
        return items;
    }

    public async Task<RetrivedItemDto?> RetriveByIdAsync(int id)
    {
        return _mapper.Map<RetrivedItemDto?>(await _repo.RetriveByIdAsync(id));
    }

    public async Task<bool?> UpdateAsync(int id, UpdatedItemDto item)
    {
        Item? existing = await _repo.RetriveByIdAsync(id);
        if (existing is null) return null;

        Item updatedItem = _mapper.Map(item, existing);
        
        updatedItem.UpdatedAt = DateTime.UtcNow;
        
        Item? updated = await _repo.UpdateAsync(id, updatedItem);
        if (updated is null) return false;
        else return true;
    }

    public async Task<bool?> DeleteAsync(int id)
    {
        Item? item = await _repo.RetriveByIdAsync(id);
        if (item is null) return null;
        return await _repo.DeleteAsync(item);
    }
}