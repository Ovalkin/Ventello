using Vintello.Common.DTOs;

namespace Vintello.Services;

public interface IItemService
{
    Task<CreatedItemDto?> CreateAsync(CreatedItemDto item);
    Task<IEnumerable<RetrivedItemsDto>> RetriveByParamsAsync(string? status, int? user, int? category);
    Task<RetrivedItemsDto?> RetriveByIdAsync(int id);
    Task<RetrivedItemsDto?> UpdateAsync(int id, UpdatedItemDto updatedItemDto);
    Task<bool?> DeleteAsync(int id);
}