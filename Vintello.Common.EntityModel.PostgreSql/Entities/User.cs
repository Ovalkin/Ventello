using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vintello.Common.EntityModel.PostgreSql;

[Table("users")]
public partial class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("role_id")]
    public int RoleId { get; set; }

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
    [StringLength(255)]
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
    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual Role Role { get; set; } = null!;
}
