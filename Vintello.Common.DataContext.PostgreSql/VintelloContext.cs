using System.Globalization;
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
                                    " database=postgres;" +
                                    " username=postgres;" +
                                    " password=7878;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql(DateTime.Now.ToString(CultureInfo.InvariantCulture));
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}