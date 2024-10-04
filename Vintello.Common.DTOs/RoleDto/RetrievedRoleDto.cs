namespace Vintello.Common.DTOs;

public class RetrievedRoleDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public List<RetrievedUsersDto> Users { get; set; } = new();
}
