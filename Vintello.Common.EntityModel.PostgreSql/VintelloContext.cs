using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Vintello.Common.EntityModel.PostgreSql;

public partial class VintelloContext : DbContext
{
    public VintelloContext()
    {
    }
    
    [Obsolete("Obsolete")]
    static VintelloContext()
        => NpgsqlConnection.GlobalTypeMapper
            .MapEnum<RolesEnum>();


    public VintelloContext(DbContextOptions<VintelloContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(
            "host=localhost; port=5432; database=vintello90;  username=postgres;  password=7878;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<RolesEnum>();
        
        modelBuilder.HasSequence<int>("categories_id_seq").StartsAt(1).IncrementsBy(1);
        modelBuilder.HasSequence<int>("items_id_seq").StartsAt(1).IncrementsBy(1);
        modelBuilder.HasSequence<int>("users_id_seq").StartsAt(1).IncrementsBy(1);
        
        modelBuilder.Entity<Category>().Property(f => f.Id).HasDefaultValueSql("nextval('categories_id_seq'::regclass)");
        modelBuilder.Entity<Item>().Property(f => f.Id).HasDefaultValueSql("nextval('items_id_seq'::regclass)");
        modelBuilder.Entity<User>().Property(f => f.Id).HasDefaultValueSql("nextval('users_id_seq'::regclass)");
        
        modelBuilder.Entity<Category>(entity => { entity.HasKey(e => e.Id).HasName("categories_pkey"); });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("items_pkey");

            entity.Property(e => e.Status).HasDefaultValueSql("'created'::character varying");

            entity.HasOne(d => d.Category).WithMany(p => p.Items).HasConstraintName("items_category_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Items).HasConstraintName("items_user_id_fkey");
        });
        
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}