namespace Vintello.Common.DTOs;

public class CreatedUserDto
{
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string Password { get; set; } = null!;
    public string? Location { get; set; }
    public string? ProfilePic { get; set; }
}

public class RetrivedUsersDto
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
}

public class RetrivedUserDto
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
    public List<RetrivedItemDto> Items { get; set; } = new();
}

public class UpdatedUserDto
{
    public int? RoleId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Phone { get; set; }
    public string? Location { get; set; }
    public string? ProfilePic { get; set; }
    public string? Bio { get; set; }
}