using CSM_Foundation.Source.Bases;
using CSM_Foundation.Source.Interfaces;
using CSM_Foundation.Source.Validators;

using Microsoft.EntityFrameworkCore;

namespace TWS_Business.Sets;

public partial class Truck
    : BSourceSet {
    public override int Id { get; set; }

    public string Vin { get; set; } = null!;

    public int Manufacturer { get; set; }

    public string Motor { get; set; } = null!;

    public int? Sct { get; set; }

    public int? Maintenance { get; set; }

    public int? Situation { get; set; }

    public int? Insurance { get; set; }

    public virtual Insurance? InsuranceNavigation { get; set; }

    public virtual Maintenance? MaintenanceNavigation { get; set; }

    public virtual Manufacturer? ManufacturerNavigation { get; set; }

    public virtual Sct? SctNavigation { get; set; }

    public virtual Situation? SituationNavigation { get; set; }

    public virtual ICollection<Plate> Plates { get; set; } = [];

    public static void Set(ModelBuilder builder) {
        _ = builder.Entity<Truck>(entity => {
            _ = entity.HasKey(e => e.Id);

            _ = entity.HasIndex(e => e.Vin)
                .IsUnique();
            _ = entity.HasIndex(e => e.Motor)
                .IsUnique();

            _ = entity.Property(e => e.Id)
                .HasColumnName("id");
            _ = entity.Property(e => e.Motor)
                .HasMaxLength(16)
                .IsUnicode(false);
            _ = entity.Property(e => e.Sct)
                .HasColumnName("SCT");
            _ = entity.Property(e => e.Vin)
                .HasMaxLength(17)
                .IsUnicode(false)
                .HasColumnName("VIN");

            _ = entity.HasOne(d => d.InsuranceNavigation)
                .WithMany(p => p.Trucks)
                .HasForeignKey(d => d.Insurance);

            _ = entity.HasOne(d => d.MaintenanceNavigation)
                .WithMany(p => p.Trucks)
                .HasForeignKey(d => d.Maintenance);
            _ = entity.HasOne(d => d.ManufacturerNavigation).WithMany(p => p.Trucks)
                .HasForeignKey(d => d.Manufacturer)
                .OnDelete(DeleteBehavior.ClientSetNull);
            _ = entity.HasOne(d => d.SctNavigation)
                .WithMany(p => p.Trucks)
                .HasForeignKey(d => d.Sct);
            _ = entity.HasOne(d => d.SituationNavigation)
                .WithMany(p => p.Trucks)
                .HasForeignKey(d => d.Situation);
        });
    }

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container) {
        UniqueValidator Unique = new();
        PointerValidator Pointer = new(true, false);
        Container = [
            ..Container,
            (nameof(Vin), [Unique, new LengthValidator(17, 17)]),
            (nameof(Manufacturer), [Unique, new PointerValidator(true)]),
            (nameof(Motor), [Unique, new LengthValidator(15, 16)]),
            (nameof(Sct), [Unique, Pointer]),
            (nameof(Maintenance), [Pointer]),
            (nameof(Situation), [Pointer]),
            (nameof(Insurance), [Pointer]),
        ];
        return Container;
    }
}
