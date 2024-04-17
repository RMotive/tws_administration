using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TWS_Business.Sets;

namespace TWS_Business;

public partial class TWSBusinessSource
{
    public TWSBusinessSource(DbContextOptions<TWSBusinessSource> options)
        : base(options)
    {
    }

    public virtual DbSet<Insurance> Insurances { get; set; }

    public virtual DbSet<Maintenance> Maintenances { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Plate> Plates { get; set; }

    public virtual DbSet<Situation> Situations { get; set; }

    public virtual DbSet<Truck> Trucks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Insurance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Insuranc__3213E83FF5D07FAA");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Country)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.Policy)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Maintenance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Maintena__3213E83F493363B3");

            entity.Property(e => e.Id).HasColumnName("id");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Manufact__3213E83FE99242F3");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Brand)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Model)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Plate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Plates__3213E83F8192257A");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Country)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.Identifier)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(3)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Situation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Situatio__3213E83F127DE4E7");

            entity.HasIndex(e => e.Name, "UQ__Situatio__737584F658ECB81C").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(25);
        });

        modelBuilder.Entity<Truck>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Trucks__3213E83FD231B178");

            entity.HasIndex(e => e.Vin, "UQ__Trucks__C5DF234CA048AA71").IsUnique();

            entity.HasIndex(e => e.Sct, "UQ__Trucks__CA1908069B670C73").IsUnique();

            entity.HasIndex(e => e.Motor, "UQ__Trucks__FF113ED41643C6D3").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Motor)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.Sct)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("SCT");
            entity.Property(e => e.Vin)
                .HasMaxLength(17)
                .IsUnicode(false)
                .HasColumnName("VIN");

            entity.HasOne(d => d.InsuranceNavigation).WithMany(p => p.Trucks)
                .HasForeignKey(d => d.Insurance)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK@Trucks_Insurances");

            entity.HasOne(d => d.MaintenanceNavigation).WithMany(p => p.Trucks)
                .HasForeignKey(d => d.Maintenance)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK@Trucks_Maintenances");

            entity.HasOne(d => d.ManufacturerNavigation).WithMany(p => p.Trucks)
                .HasForeignKey(d => d.Manufacturer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK@Trucks_Manufacturers");

            entity.HasOne(d => d.PlateNavigation).WithMany(p => p.Trucks)
                .HasForeignKey(d => d.Plate)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK@Trucks_Plates");

            entity.HasOne(d => d.SituationNavigation).WithMany(p => p.Trucks)
                .HasForeignKey(d => d.Situation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK@Trucks_Situations");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
