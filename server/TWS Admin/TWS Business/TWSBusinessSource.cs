using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

    public virtual DbSet<Sct> Scts { get; set; }

    public virtual DbSet<Situation> Situations { get; set; }

    public virtual DbSet<Truck> Trucks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        optionsBuilder.UseSqlServer("Server=DESKTOP-M2SPTNQ;Database=TWS Business; Trusted_Connection=True; Encrypt=False");

        optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddDebug()))
                             .EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Insurance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Insuranc__3213E83FFEFAED23");

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
            entity.HasKey(e => e.Id).HasName("PK__Maintena__3213E83FE8A37BE0");

            entity.Property(e => e.Id).HasColumnName("id");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Manufact__3213E83F65F8F6B2");

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
            entity.HasKey(e => e.Id).HasName("PK__Plates__3213E83F1DAA3B62");

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

            entity.HasOne(d => d.TruckNavigation).WithMany(p => p.Plates)
                .HasForeignKey(d => d.Truck)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK@Plates_Trucks");
        });

        modelBuilder.Entity<Sct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SCT__3213E83FD096EF98");

            entity.ToTable("SCT");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Configuration)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Number)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(6)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Situation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Situatio__3213E83F5226D2F0");

            entity.HasIndex(e => e.Name, "UQ__Situatio__737584F603C70368").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(25);
        });

        modelBuilder.Entity<Truck>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Trucks__3213E83FF8FEE6CB");

            entity.HasIndex(e => e.Maintenance, "UQ__Trucks__32D1185F53A7D0F2").IsUnique();

            entity.HasIndex(e => e.Insurance, "UQ__Trucks__93D492550C6EA2D0").IsUnique();

            entity.HasIndex(e => e.Vin, "UQ__Trucks__C5DF234C77595224").IsUnique();

            entity.HasIndex(e => e.Sct, "UQ__Trucks__CA190806B354828D").IsUnique();

            entity.HasIndex(e => e.Motor, "UQ__Trucks__FF113ED4BA7E44C4").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Motor)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.Sct).HasColumnName("SCT");
            entity.Property(e => e.Vin)
                .HasMaxLength(17)
                .IsUnicode(false)
                .HasColumnName("VIN");

            entity.HasOne(d => d.InsuranceNavigation).WithOne(p => p.Truck)
                .HasForeignKey<Truck>(d => d.Insurance)
                .HasConstraintName("FK@Trucks_Insurances");

            entity.HasOne(d => d.MaintenanceNavigation).WithOne(p => p.Truck)
                .HasForeignKey<Truck>(d => d.Maintenance)
                .HasConstraintName("FK@Trucks_Maintenances");

            entity.HasOne(d => d.ManufacturerNavigation).WithMany(p => p.Trucks)
                .HasForeignKey(d => d.Manufacturer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK@Trucks_Manufacturers");

            entity.HasOne(d => d.SctNavigation).WithOne(p => p.Truck)
                .HasForeignKey<Truck>(d => d.Sct)
                .HasConstraintName("FK@Trucks_SCT");

            entity.HasOne(d => d.SituationNavigation).WithMany(p => p.Trucks)
                .HasForeignKey(d => d.Situation)
                .HasConstraintName("FK@Trucks_Situations");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
