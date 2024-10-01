using System.ComponentModel.DataAnnotations;

namespace Vintello.Common.DTOs;

public class CreatedRolesDto
{
    [Required(ErrorMessage = "Имя роли обязтельно!")]
    [RegularExpression(@"^\D*$", ErrorMessage = "Имя может содержать только слова!")]
    public string Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
}

public class RetrievedRolesDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
}

public class RetrievedRoleDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public List<RetrievedUsersDto> Users { get; set; } = new();
}

public class UpdatedRoleDto
{
    [RegularExpression(@"^\D*$", ErrorMessage = "Имя может содержать только слова!")]
    public string? Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
}