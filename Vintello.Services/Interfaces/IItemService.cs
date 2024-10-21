using Vintello.Common.DTOs;

namespace Vintello.Services;

public interface IItemService
{
    Task<RetrievedItemDto?> CreateAsync(CreatedItemDto item, string userId, string userRole);
    Task<RetrievedItemDto?> RetrieveByIdAsync(int id);
    Task<IEnumerable<RetrievedItemDto>> RetrieveAsync(string? status, int? user, int? category);
    Task<bool?> UpdateAsync(int id, UpdatedItemDto updatedItemDto, string userId, string userRole);
    Task<bool?> DeleteAsync(int id, string userId, string userRole);
}