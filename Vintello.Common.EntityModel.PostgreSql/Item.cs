using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vintello.Common.EntityModel.PostgreSql;

[Table("item")]
public partial class Item
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("category_id")]
    public int CategoryId { get; set; }

    [Column("title", TypeName = "character varying")]
    public string Title { get; set; } = null!;

    [Column("status", TypeName = "character varying")]
    public string Status { get; set; } = null!;

    [Column("price", TypeName = "money")]
    public decimal? Price { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("images")]
    public List<string>? Images { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Items")]
    public virtual Category Category { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Items")]
    public virtual User User { get; set; } = null!;
}
