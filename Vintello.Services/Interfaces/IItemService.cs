using Vintello.Common.DTOs;

namespace Vintello.Services;

public interface IItemService
{
    Task<RetrievedItemDto?> CreateAsync(CreatedItemDto item);
    Task<RetrievedItemDto?> RetrieveByIdAsync(int id);
    Task<IEnumerable<RetrievedItemDto>> RetrieveAsync(string? status, int? user, int? category);
    Task<bool?> UpdateAsync(int id, UpdatedItemDto updatedItemDto);
    Task<bool?> DeleteAsync(int id);
}