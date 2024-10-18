using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.DTOs;

public class RetrievedUsersDto
{
    public int Id { get; set; }
    public RolesEnum Role { get; set; }
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Location { get; set; }
    public string? ProfilePic { get; set; }
    public string? Bio { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public static IEnumerable<RetrievedUsersDto> CreateDto(IEnumerable<User> users)
    {
        List<RetrievedUsersDto> result = new();
        foreach (var user in users)
        {
            result.Add(new RetrievedUsersDto
            {
                Id = user.Id,
                Role = user.Role,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Location = user.Location,
                ProfilePic = user.ProfilePic,
                Bio = user.Bio,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
            });
        }
        return result.AsEnumerable();
    }
}
