namespace Vintello.Common.DTOs;

public class CreateUserDto
{
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string Password { get; set; } = null!;
    public string? Location { get; set; }
    public string? ProfilePic { get; set; }
}

public class RetriveUserDto
{
    public int Id { get; set; }
    public string Role { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public string? Location { get; set; }
    public string? ProfilePic { get; set; }
    public string? Bio { get; set; }
}