using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vintello.Common.EntityModel.PostgreSql;

[Table("users")]
public partial class User
{
    [Key]
    [Column("id")]
    [Required]
    public int Id { get; set; }

    [Column("role")] 
    [StringLength(255)] 
    public string? Role { get; set; } = "client";

    [Column("first_name")]
    [StringLength(255)]
    [Required]
    public string FirstName { get; set; } = null!;

    [Column("last_name")]
    [StringLength(255)]
    public string? LastName { get; set; }

    [Column("email")]
    [StringLength(255)]
    [Required]
    public string Email { get; set; } = null!;

    [Column("phone")]
    [StringLength(20)]
    public string? Phone { get; set; }

    [Column("password")]
    [StringLength(255)]
    [Required]
    public string Password { get; set; } = null!;

    [Column("location")]
    [StringLength(255)]
    public string? Location { get; set; }

    [Column("created_at", TypeName = "timestamp with time zone")]
    public DateTime? CreatedAt { get; set; }

    [Column("profile_pic")]
    [StringLength(255)]
    public string? ProfilePic { get; set; }

    [Column("bio")]
    public string? Bio { get; set; }
}
