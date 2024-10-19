using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vintello.Common.EntityModel.PostgreSql;

[Table("users")]
[Index("Email", Name = "users_email_key", IsUnique = true)]
[Index("Phone", Name = "users_phone_key", IsUnique = true)]
public sealed class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("role")]
    public RolesEnum Role { get; set; }

    [Column("first_name")]
    [StringLength(255)]
    public string FirstName { get; set; } = null!;

    [Column("last_name")]
    [StringLength(255)]
    public string? LastName { get; set; }

    [Column("email")]
    [StringLength(255)]
    public string Email { get; set; } = null!;

    [Column("phone")]
    [StringLength(20)]
    public string? Phone { get; set; }

    [Column("password")]
    [StringLength(255)]
    public string Password { get; set; } = null!;

    [Column("location")]
    [StringLength(255)]
    public string? Location { get; set; }

    [Column("profile_pic")]
    [StringLength(255)]
    public string? ProfilePic { get; set; }

    [Column("bio")]
    public string? Bio { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("User")]
    public ICollection<Item> Items { get; set; } = new List<Item>();
}
