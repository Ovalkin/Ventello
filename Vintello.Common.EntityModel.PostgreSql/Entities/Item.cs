using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vintello.Common.EntityModel.PostgreSql;

[Table("items")]
public partial class Item
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("category_id")]
    public int CategoryId { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string Title { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [Column("status")]
    [StringLength(255)]
    public string Status { get; set; } = null!;

    [Column("images")]
    public List<string>? Images { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("price")]
    public int? Price { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Items")]
    public virtual Category Category { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Items")]
    public virtual User User { get; set; } = null!;
}
