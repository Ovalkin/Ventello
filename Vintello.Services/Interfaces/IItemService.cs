using Vintello.Common.DTOs;

namespace Vintello.Services;

public interface IItemService
{
    Task<RetrivedItemDto?> CreateAsync(CreatedItemDto item);
    Task<RetrivedItemDto?> RetrieveByIdAsync(int id);
    Task<IEnumerable<RetrivedItemDto>> RetrieveAsync(string? status, int? user, int? category);
    Task<bool?> UpdateAsync(int id, UpdatedItemDto updatedItemDto);
    Task<bool?> DeleteAsync(int id);
}