using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.DTOs;

public class RetrievedRolesDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public static IEnumerable<RetrievedRolesDto> CreateDto(IEnumerable<Role> roles)
    {
        List<RetrievedRolesDto> result = new();
        foreach (var role in roles)
        {
            result.Add(new RetrievedRolesDto()
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            });
        }
        return result.AsEnumerable();
    }
}