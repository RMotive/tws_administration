using Microsoft.EntityFrameworkCore;

using TWS_Security.Sets;

namespace TWS_Security;

public partial class TWSSecuritySource {
    public TWSSecuritySource(DbContextOptions<TWSSecuritySource> options)
        : base(options) {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountsPermit> AccountsPermits { get; set; }

    public virtual DbSet<Feature> Features { get; set; }

    public virtual DbSet<Permit> Permits { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<ProfilesPermit> ProfilesPermits { get; set; }

    public virtual DbSet<Solution> Solutions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Account>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__Accounts__3213E83F365E950F");

            entity.HasIndex(e => e.User, "UQ__Accounts__7FC76D727B35E61A").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.User)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user");
        });

        modelBuilder.Entity<AccountsPermit>(entity => {
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

        modelBuilder.Entity<Feature>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__Features__3213E83F264D9ED7");

            entity.HasIndex(e => e.Name, "UQ__Features__737584F660DC9B98").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(25);
        });

        modelBuilder.Entity<Permit>(entity => {
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

        modelBuilder.Entity<Profile>(entity => {
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

        modelBuilder.Entity<ProfilesPermit>(entity => {
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

        modelBuilder.Entity<Solution>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__Solution__3213E83F2D355DCB");

            entity.HasIndex(e => e.Sign, "UQ__Solution__2F82F0C89DFAB179").IsUnique();

            entity.HasIndex(e => e.Name, "UQ__Solution__72E12F1B81AEDEF1").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Sign)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("sign");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
