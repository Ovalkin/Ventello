using Vintello.Common.DTOs;

namespace Vintello.Services;

public interface IItemService
{
    Task<RetrivedItemDto?> CreateAsync(CreatedItemDto item);
    Task<IEnumerable<RetrivedItemDto>> RetriveByParamsAsync(string? status, int? user, int? category);
    Task<RetrivedItemDto?> RetriveByIdAsync(int id);
    Task<bool?> UpdateAsync(int id, UpdatedItemDto updatedItemDto);
    Task<bool?> DeleteAsync(int id);
}