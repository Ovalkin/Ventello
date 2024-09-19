using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vintello.Common.EntityModel.PostgreSql;

[Table("roles")]
public partial class Role
{
    [Key]
    [Column("role_name")]
    [StringLength(255)]
    public string RoleName { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [InverseProperty("RoleNavigation")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
