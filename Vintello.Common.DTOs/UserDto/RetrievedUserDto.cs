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
}