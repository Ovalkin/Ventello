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
    public async Task<RetrivedUserDto?> CreateAsync(CreatedUserDto createUserDto)
    {
        User user = _mapper.Map<User>(createUserDto);
        user.CreatedAt = DateTime.UtcNow;
        return _mapper.Map<RetrivedUserDto?>(await _repo.CreateAsync(user));
    }

    public async Task<IEnumerable<RetrivedUsersDto>> RetrieveAsync(string? location)
    {
        List<RetrivedUsersDto> users = _mapper.Map<List<RetrivedUsersDto>>(await _repo.RetrieveAllAsync());
        if (string.IsNullOrWhiteSpace(location)) return users;
        location = location.ToLower();
        return users.Where(user => user.Location == location);
    }

    public async Task<RetrivedUserDto?> RetrieveByIdAsync(int id)
    {
        return _mapper.Map<RetrivedUserDto>(await _repo.RetrieveByIdAsync(id));
    }

    public async Task<bool?> UpdateAsync(int id, UpdatedUserDto user)
    {
        User? existing = await _repo.RetrieveByIdAsync(id);
        if (existing is null) return null;
        
        User? updatedUser = _mapper.Map(user, existing);

        User? updated = await _repo.UpdateAsync(id, updatedUser);
        if (updated is null) return false;
        else return true;
    }

    public async Task<bool?> DeleteAsync(int id)
    {
        User? user = await _repo.RetrieveByIdAsync(id);
        if (user is null) return null;
        return await _repo.DeleteAsync(user);
    }
}