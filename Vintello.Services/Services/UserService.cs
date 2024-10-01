using AutoMapper;
using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;
using Vintello.Common.Repositories;

namespace Vintello.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;
    private readonly IMapper _mapper;

    public UserService(IUserRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<RetrievedUserDto?> CreateAsync(CreatedUserDto createUserDto)
    {
        User user = _mapper.Map<User>(createUserDto);
        user.CreatedAt = DateTime.UtcNow;
        return _mapper.Map<RetrievedUserDto?>(await _repo.CreateAsync(user));
    }

    public async Task<IEnumerable<RetrievedUsersDto>> RetrieveAsync(string? location)
    {
        List<RetrievedUsersDto> users = _mapper.Map<List<RetrievedUsersDto>>(await _repo.RetrieveAllAsync());
        if (string.IsNullOrWhiteSpace(location)) return users;
        location = location.ToLower();
        return users.Where(user => user.Location == location);
    }

    public async Task<RetrievedUserDto?> RetrieveByIdAsync(int id)
    {
        return _mapper.Map<RetrievedUserDto>(await _repo.RetrieveByIdAsync(id));
    }

    public async Task<bool?> UpdateAsync(int id, UpdatedUserDto user)
    {
        User? existing = await _repo.RetrieveByIdAsync(id);
        if (existing is null) return null;
        int roleId = existing.RoleId;
        
        User? updatedUser = _mapper.Map(user, existing);
        updatedUser.RoleId = roleId;
        User? updated = await _repo.UpdateAsync(id, updatedUser);
        if (updated is null) return false;
        return true;
    }

    public async Task<bool?> DeleteAsync(int id)
    {
        User? user = await _repo.RetrieveByIdAsync(id);
        if (user is null) return null;
        return await _repo.DeleteAsync(user);
    }
}