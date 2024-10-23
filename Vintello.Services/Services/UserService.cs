using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;
using Vintello.Common.Repositories;

namespace Vintello.Services;

public class UserService(IUserRepository repo) : IUserService
{
    public async Task<RetrievedUserDto?> CreateAsync(CreatedUserDto createUserDto)
    {
        User user = CreatedUserDto.CreateUser(createUserDto);
        return RetrievedUserDto.CreateDto((await repo.CreateAsync(user))!);
    }

    public async Task<IEnumerable<RetrievedUsersDto>> RetrieveAsync(string? location)
    {
        List<RetrievedUsersDto> users = RetrievedUsersDto.CreateDto(await repo.RetrieveAllAsync()).ToList();
        if (string.IsNullOrWhiteSpace(location)) return users;
        return users.Where(user => user.Location == location);
    }

    public async Task<RetrievedUserDto?> RetrieveByIdAsync(int id)
    {
        User? user = await repo.RetrieveByIdAsync(id);
        if (user is null) return null;
        return RetrievedUserDto.CreateDto(user);
    }

    public async Task<bool?> UpdateAsync(int id, UpdatedUserDto user, string userRole, string userId)
    {
        if (userRole == "Client" && userId != id.ToString())
            return false;
        
        User? existing = await repo.RetrieveByIdAsync(id);
        if (existing is null) return null;
        
        existing = UpdatedUserDto.CreateUser(user, existing);

        bool? updated = await repo.UpdateAsync(id, existing);
        if (updated is null) return false;
        return true;
    }

    public async Task<bool?> DeleteAsync(int id, string userRole, string userId)
    {
        if (userRole == "Client" && userId != id.ToString())
            return null;
        
        User? user = await repo.RetrieveByIdAsync(id);
        if (user is null) return null;
        return await repo.DeleteAsync(user);
    }
}