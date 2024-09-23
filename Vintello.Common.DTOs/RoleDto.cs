namespace Vintello.Common.DTOs;

public class CreatedUpdatedRetrivedRolesDto
{
    public string RoleName { get; set; } = null!;
    public string? Description { get; set; } = null!;
}

public class RetrivedRoleDto
{
    public string RoleName { get; set; } = null!;
    public string? Description { get; set; }
    public List<RetrivedUsersDto> Users { get; set; } = new();
}