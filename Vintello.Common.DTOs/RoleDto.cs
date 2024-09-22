namespace Vintello.Common.DTOs;

public class CreatedUpdatedRetrivedRolesDto
{
    public string RoleName { get; set; } = null!;
    public string? Description { get; set; } = null!;
}

public class RetriveRoleDto
{
    public string RoleName { get; set; } = null!;
    public string? Description { get; set; }
    public List<RetriveUserDto> Users { get; set; } = new();
}