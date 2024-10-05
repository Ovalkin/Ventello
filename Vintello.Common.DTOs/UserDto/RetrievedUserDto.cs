using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.DTOs;

public class RetrievedUserDto
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Location { get; set; }
    public string? ProfilePic { get; set; }
    public string? Bio { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public List<RetrievedItemDto> Items { get; set; } = new();

    public static RetrievedUserDto CreateDto(User user)
    {
        return new RetrievedUserDto
        {
            Id = user.Id,
            RoleId = user.RoleId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Phone = user.Phone,
            Location = user.Location,
            ProfilePic = user.ProfilePic,
            Bio = user.Bio,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
            Items = (List<RetrievedItemDto>)RetrievedItemDto.CreateDto(user.Items)
        };
    }
}