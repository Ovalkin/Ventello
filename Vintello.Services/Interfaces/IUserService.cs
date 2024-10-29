using Vintello.Common.DTOs;

namespace Vintello.Services;

public interface IUserService
{
    Task<RetrievedUserDto?> CreateAsync(CreatedUserDto user);
    Task<RetrievedUserDto?> RetrieveByIdAsync(int id);
    Task<IEnumerable<RetrievedUsersDto>> RetrieveAsync(string? location);
    Task<bool?> UpdateAsync(int id, UpdatedUserDto updatedUserDto);
    Task<bool?> DeleteAsync(int id);
}