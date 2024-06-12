using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TWS_Security.Sets;

namespace TWS_Security;

public partial class TWSSecuritySource
{
    public TWSSecuritySource(DbContextOptions<TWSSecuritySource> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountsPermit> AccountsPermits { get; set; }

    public virtual DbSet<Feature> Features { get; set; }

    public virtual DbSet<Permit> Permits { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<ProfilesPermit> ProfilesPermits { get; set; }

    public virtual DbSet<Solution> Solutions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Accounts__3213E83F52C4F6B5");

            entity.HasIndex(e => e.User, "UQ__Accounts__7FC76D72D0A4E857").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.User)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AccountsPermit>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Accounts_Permits");

            entity.Property(e => e.Account).HasColumnName("account");
            entity.Property(e => e.Permit).HasColumnName("permit");

            entity.HasOne(d => d.AccountNavigation).WithMany()
                .HasForeignKey(d => d.Account)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Accounts___accou__48CFD27E");

            entity.HasOne(d => d.PermitNavigation).WithMany()
                .HasForeignKey(d => d.Permit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Accounts___permi__4AB81AF0");
        });

        modelBuilder.Entity<Feature>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Features__3213E83F2A46DA99");

            entity.HasIndex(e => e.Name, "UQ__Features__737584F625780477").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(25);
        });

        modelBuilder.Entity<Permit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permits__3213E83F4CC021D3");

            entity.HasIndex(e => e.Reference, "UQ__Permits__062B9EB8AF64EF0B").IsUnique();

            entity.HasIndex(e => e.Name, "UQ__Permits__72E12F1B8E8BD02F").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Reference)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.SolutionNavigation).WithMany(p => p.Permits)
                .HasForeignKey(d => d.Solution)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Permits__solutio__4CA06362");
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Profiles__3213E83F517D120E");

            entity.HasIndex(e => e.Name, "UQ__Profiles__72E12F1B8304B5D0").IsUnique();

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
                .HasConstraintName("FK__Profiles___permi__4E88ABD4");

            entity.HasOne(d => d.ProfileNavigation).WithMany()
                .HasForeignKey(d => d.Profile)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Profiles___profi__5070F446");
        });

        modelBuilder.Entity<Solution>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Solution__3213E83F15F6F9CC");

            entity.HasIndex(e => e.Sign, "UQ__Solution__2F82F0C810C5059F").IsUnique();

            entity.HasIndex(e => e.Name, "UQ__Solution__72E12F1BA5A68A97").IsUnique();

            entity.HasIndex(e => e.Sign, "U_Sign").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Sign)
                .HasMaxLength(5)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
