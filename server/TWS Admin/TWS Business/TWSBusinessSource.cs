using Foundation.Managers;
using Foundation.Models;
using Microsoft.EntityFrameworkCore;
using TWS_Business.Sets;

namespace TWS_Business;

public partial class TWSBusinessSource 
: DbContext{
    private readonly DatasourceConnectionModel ConnectionProperties;

    public TWSBusinessSource()
    {
        ConnectionProperties = DatasourceConnectionManager.Load();
    }

    public TWSBusinessSource(DbContextOptions<TWSBusinessSource> options)
        : base(options)
    {
        ConnectionProperties = DatasourceConnectionManager.Load();
    }

    public virtual DbSet<Truck> Truck { get; set; }

    public virtual DbSet<Plates> Plates { get; set; }

    public virtual DbSet<Manufacturers> Manufacturers { get; set; }

    public virtual DbSet<Maintenance> Maintenance { get; set; }

    public virtual DbSet<Insurances> Insurances { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string ConnectionString =
            $"Server={ConnectionProperties.Host};" +
            $"Database={ConnectionProperties.Database};" +
            $"User={ConnectionProperties.User};" +
            $"Password={ConnectionProperties.Password};" +
            $"Encrypt={ConnectionProperties.Encrypted};";


        optionsBuilder.UseSqlServer(ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Truck>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__Accounts__3213E83F365E950F");

            entity.HasIndex(e => e.User, "UQ__Accounts__7FC76D727B35E61A").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Wildcard).HasColumnName("wildcard");
            entity.Property(e => e.User)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user");
        });

        modelBuilder.Entity<Plates>(entity => {
            entity
                .HasNoKey()
                .ToTable("Accounts_Permits");

            entity.Property(e => e.Account).HasColumnName("account");
            entity.Property(e => e.Permit).HasColumnName("permit");

            entity.HasOne(d => d.AccountNavigation).WithMany()
                .HasForeignKey(d => d.Account)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Accounts___accou__46E78A0C");

            entity.HasOne(d => d.PermitNavigation).WithMany()
                .HasForeignKey(d => d.Permit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Accounts___permi__47DBAE45");
        });

        modelBuilder.Entity<Manufacturers>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__Permits__3213E83F49C6A78D");

            entity.HasIndex(e => e.Name, "UQ__Permits__72E12F1BAEA504F0").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Solution).HasColumnName("solution");

            entity.HasOne(d => d.SolutionNavigation).WithMany(p => p.Permits)
                .HasForeignKey(d => d.Solution)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Permits__solutio__3C69FB99");
        });

        modelBuilder.Entity<Maintenance>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__Profiles__3213E83FC4B7E671");

            entity.HasIndex(e => e.Name, "UQ__Profiles__72E12F1B00165EF3").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Insurances>(entity => {
            entity
                .HasNoKey()
                .ToTable("Profiles_Permits");

            entity.Property(e => e.Permit).HasColumnName("permit");
            entity.Property(e => e.Profile).HasColumnName("profile");

            entity.HasOne(d => d.PermitNavigation).WithMany()
                .HasForeignKey(d => d.Permit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Profiles___permi__412EB0B6");

            entity.HasOne(d => d.ProfileNavigation).WithMany()
                .HasForeignKey(d => d.Profile)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Profiles___profi__4222D4EF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
