using System.ComponentModel.DataAnnotations;
using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.DTOs;

public class CreatedRoleDto
{
    [Required(ErrorMessage = "Имя роли обязтельно!")]
    [RegularExpression(@"^\D*$", ErrorMessage = "Имя может содержать только слова!")]
    public string Name { get; set; } = null!;
    public string? Description { get; set; } = null!;

    public static Role CreateRole(CreatedRoleDto createdRoleDto)
    {
        return new Role
        {
            Name = createdRoleDto.Name,
            Description = createdRoleDto.Description
        };
    }
}