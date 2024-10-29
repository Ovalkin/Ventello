using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;
using Vintello.Repositories;

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
        return string.IsNullOrWhiteSpace(location) ? users : users.Where(user => user.Location == location);
    }

    public async Task<RetrievedUserDto?> RetrieveByIdAsync(int id)
    {
        User? user = await repo.RetrieveByIdAsync(id);
        return user is null ? null : RetrievedUserDto.CreateDto(user);
    }

    public async Task<bool?> UpdateAsync(int id, UpdatedUserDto user)
    {
        User? existing = await repo.RetrieveByIdAsync(id);
        if (existing is null) return null;
        existing = UpdatedUserDto.CreateUser(user, existing);
        bool? updated = await repo.UpdateAsync(id, existing);
        return updated is not null;
    }

    public async Task<bool?> DeleteAsync(int id)
    {
        User? user = await repo.RetrieveByIdAsync(id);
        if (user is null) return null;
        return await repo.DeleteAsync(user);
    }
}