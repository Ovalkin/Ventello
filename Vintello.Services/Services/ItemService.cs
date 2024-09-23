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
    
    
}