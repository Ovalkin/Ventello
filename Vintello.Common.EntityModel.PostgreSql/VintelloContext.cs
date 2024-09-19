using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Vintello.Common.EntityModel.PostgreSql;

public partial class VintelloContext : DbContext
{
    public VintelloContext()
    {
    }

    public VintelloContext(DbContextOptions<VintelloContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("host=localhost;" +
                                    " port=5432;" +
                                    " database=vintello;" +
                                    "  username=postgres;" +
                                    "  password=7878;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.Property(e => e.Role).HasDefaultValueSql("'client'::character varying");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
