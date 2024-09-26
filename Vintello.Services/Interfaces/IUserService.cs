using Vintello.Common.DTOs;

namespace Vintello.Services;

public interface IUserService
{
    Task<RetrivedUserDto?> CreateAsync(CreatedUserDto user);
    Task<RetrivedUserDto?> RetrieveByIdAsync(int id);
    Task<IEnumerable<RetrivedUsersDto>> RetrieveAsync(string? location);
    Task<bool?> UpdateAsync(int id, UpdatedUserDto updatedUserDto);
    Task<bool?> DeleteAsync(int id);
}