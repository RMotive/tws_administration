
using Microsoft.EntityFrameworkCore;
using TWS_Business.Sets;

namespace TWS_Business;

public partial class TWSBusinessSource{

    public TWSBusinessSource(DbContextOptions<TWSBusinessSource> options)
        : base(options){
    }

    public virtual DbSet<Insurance> Insurances { get; set; }

    public virtual DbSet<Maintenance> Maintenances { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Plate> Plates { get; set; }

    public virtual DbSet<Truck> Trucks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Insurance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Insuranc__3213E83FC7272AD5");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Country)
                .HasMaxLength(30)
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
            entity.HasKey(e => e.Id).HasName("PK__Plates__3213E83FA07BB331");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Country)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Identifier)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Truck>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Trucks__3213E83FC4FC890E");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Motor)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.Sct)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("SCT");
            entity.Property(e => e.Situation)
                .HasMaxLength(20)
                .IsUnicode(false);
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
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
