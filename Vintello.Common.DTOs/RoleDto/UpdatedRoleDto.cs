using System.ComponentModel.DataAnnotations;
using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.DTOs;

public class UpdatedRoleDto
{
    [RegularExpression(@"^\D*$", ErrorMessage = "Имя может содержать только слова!")]
    public string? Name { get; set; } = null!;
    public string? Description { get; set; } = null!;

    public static Role CreateRole(UpdatedRoleDto updatedRoleDto, Role role)
    {
        if (updatedRoleDto.Name != null)
            role.Name = updatedRoleDto.Name;
        if (updatedRoleDto.Description != null)
            role.Description = updatedRoleDto.Description;
        return role;
    }
}