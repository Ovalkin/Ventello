namespace Vintello.Common.DTOs;

public class CreatedRolesDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
}

public class RetrivedRolesDto
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
}

public class RetrivedRoleDto
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public List<RetrivedUsersDto> Users { get; set; } = new();
}

public class UpdatedRoleDto
{
    public string? Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
}