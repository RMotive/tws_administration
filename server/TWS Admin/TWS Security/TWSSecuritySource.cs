using Microsoft.EntityFrameworkCore;
using TWS_Security.Sets;

namespace TWS_Security;

public partial class TWSSecuritySource
{
    public TWSSecuritySource(DbContextOptions<TWSSecuritySource> options)
        : base(options){
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountsPermit> AccountsPermits { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Feature> Features { get; set; }

    public virtual DbSet<Permit> Permits { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<ProfilesPermit> ProfilesPermits { get; set; }

    public virtual DbSet<Solution> Solutions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Accounts__3213E83FA692C5C2");

            entity.HasIndex(e => e.User, "UQ__Accounts__BD20C6F12EF12B23").IsUnique();

            entity.HasIndex(e => e.Contact, "UQ__Accounts__F7C046653630AE47").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.User)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.ContactNavigation).WithOne(p => p.Account)
                .HasForeignKey<Account>(d => d.Contact)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK@Accounts_Contact");
        });

        modelBuilder.Entity<AccountsPermit>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Accounts_Permits");

            entity.HasOne(d => d.AccountNavigation).WithMany()
                .HasForeignKey(d => d.Account)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK@Accounts_Permits_Accounts");

            entity.HasOne(d => d.PermitNavigation).WithMany()
                .HasForeignKey(d => d.Permit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK@Accounts_Permits_Permits");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Contact__3213E83F60CDB24B");

            entity.ToTable("Contact");

            entity.HasIndex(e => e.Phone, "UQ__Contact__5C7E359EC4E4F9C2").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Contact__A9D10534DD442408").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(14)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Feature>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Features__3213E83F8DC45FE6");

            entity.HasIndex(e => e.Name, "UQ__Features__737584F6AD8F8134").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(25);
        });

        modelBuilder.Entity<Permit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permits__3213E83FF89315D2");

            entity.HasIndex(e => e.Name, "UQ__Permits__72E12F1BB50A1500").IsUnique();

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
                .HasConstraintName("FK__Permits__solutio__403A8C7D");
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Profiles__3213E83FF05542F1");

            entity.HasIndex(e => e.Name, "UQ__Profiles__72E12F1BE680F44A").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ProfilesPermit>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Profiles_Permits");

            entity.Property(e => e.Permit).HasColumnName("permit");
            entity.Property(e => e.Profile).HasColumnName("profile");

            entity.HasOne(d => d.PermitNavigation).WithMany()
                .HasForeignKey(d => d.Permit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Profiles___permi__4AB81AF0");

            entity.HasOne(d => d.ProfileNavigation).WithMany()
                .HasForeignKey(d => d.Profile)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Profiles___profi__4CA06362");
        });

        modelBuilder.Entity<Solution>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Solution__3213E83F6F9CB0D3");

            entity.HasIndex(e => e.Sign, "UQ__Solution__2F82F0C83C859D7E").IsUnique();

            entity.HasIndex(e => e.Name, "UQ__Solution__72E12F1BB92022A0").IsUnique();

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
