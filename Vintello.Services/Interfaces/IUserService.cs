using Vintello.Common.DTOs;

namespace Vintello.Services;

public interface IUserService
{
    Task<RetrivedUserDto?> CreateAsync(CreatedUserDto createUserDto);
    Task<RetrivedUserDto?> RetriveByIdAsync(int id);
    Task<IEnumerable<RetrivedUsersDto>> RetriveAsync(string? location);
    Task<bool?> UpdateAsync(int id, UpdatedUserDto updatedUserDto);
    Task<bool?> DeleteAsync(int id);
}