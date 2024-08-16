using CSM_Foundation.Core.Bases;
using CSM_Foundation.Source.Bases;
using CSM_Foundation.Source.Interfaces;
using CSM_Foundation.Source.Validators;

using Microsoft.EntityFrameworkCore;

namespace TWS_Business.Sets;

public partial class Truck
    : BSourceSet  {
    public override int Id { get; set; }

    public int Status { get; set; }

    public string Vin { get; set; } = null!;

    public string? Motor { get; set; } = null!;

    public string Economic { get; set; } = null!;

    public int Manufacturer { get; set; }

    public int Carrier { get; set; }

    public int? Maintenance { get; set; }

    public int? Situation { get; set; }

    public int? Insurance { get; set; }

    public virtual Carrier? CarrierNavigation { get; set; }

    public virtual Insurance? InsuranceNavigation { get; set; }

    public virtual Maintenance? MaintenanceNavigation { get; set; }

    public virtual Manufacturer? ManufacturerNavigation { get; set; }

    public virtual Situation? SituationNavigation { get; set; }

    public virtual Status? StatusNavigation { get; set; }

    public virtual ICollection<Plate> Plates { get; set; } = [];

    public virtual ICollection<TruckH> TrucksH { get; set; } = [];

    public virtual ICollection<PlateH> PlatesH { get; set; } = [];

    public static void Set(ModelBuilder builder) {
        _ = builder.Entity<Truck>(entity => {
            _ = entity.HasKey(e => e.Id);

            _ = entity.Property(e => e.Id)
                .HasColumnName("id");
            _ = entity.Property(e => e.Motor)   
                .HasMaxLength(16)
                .IsUnicode(false);
          
            _ = entity.Property(e => e.Vin)
                .HasMaxLength(17)
                .IsUnicode(false)
                .HasColumnName("VIN");

            _ = entity.Property(e => e.Economic)
                .HasMaxLength(16)
                .IsUnicode(false);

            _ = entity.HasOne(d => d.CarrierNavigation)
                .WithMany(p => p.Trucks)
                .HasForeignKey(d => d.Carrier);

            _ = entity.HasOne(d => d.StatusNavigation)
                .WithMany(p => p.Trucks)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull);
            _ = entity.HasOne(d => d.InsuranceNavigation)
                .WithMany(p => p.Trucks)
                .HasForeignKey(d => d.Insurance);
            _ = entity.HasOne(d => d.MaintenanceNavigation)
                .WithMany(p => p.Trucks)
                .HasForeignKey(d => d.Maintenance);
            _ = entity.HasOne(d => d.ManufacturerNavigation)
                .WithMany(p => p.Trucks)
                .HasForeignKey(d => d.Manufacturer)
                .OnDelete(DeleteBehavior.ClientSetNull);
        
            _ = entity.HasOne(d => d.SituationNavigation)
                .WithMany(p => p.Trucks)
                .HasForeignKey(d => d.Situation)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });
    }

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container) {
        UniqueValidator Unique = new();
        Container = [
            ..Container,
            (nameof(Vin), [Unique, new LengthValidator(17, 17)]),
            (nameof(Economic), [new LengthValidator(1, 16)]),
            (nameof(Status), [new PointerValidator(true)]),
            (nameof(Carrier), [new PointerValidator(true)])


        ];
        return Container;
    }
}
