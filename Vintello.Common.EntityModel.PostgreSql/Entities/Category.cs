using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Vintello.Common.EntityModel.PostgreSql;

[Table("categories")]
public partial class Category
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [InverseProperty("Category")]
    [JsonIgnore]
    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
