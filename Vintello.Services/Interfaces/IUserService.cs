using Vintello.Common.DTOs;

namespace Vintello.Services;

public interface IUserService
{
    Task<RetrivedUserDto?> CreateAsync(CreateUserDto createUserDto);
    Task<IEnumerable<RetrivedUsersDto>> RetriveAllOrLocationUserAsync(string? location);
    Task<RetrivedUserDto?> RetriveByIdAsync(int id);
    Task<bool?> UpdateAsync(int id, UpdatedUserDto updatedUserDto);
    Task<bool?> DeleteAsync(int id);
}