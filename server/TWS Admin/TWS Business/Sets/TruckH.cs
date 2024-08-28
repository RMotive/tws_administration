

using CSM_Foundation.Core.Bases;
using CSM_Foundation.Source.Bases;
using CSM_Foundation.Source.Interfaces;
using CSM_Foundation.Source.Validators;
using Microsoft.EntityFrameworkCore;

namespace TWS_Business.Sets;
public partial class TruckH
: BHistorySourceSet {
    public override int Id { get; set; }

    public override DateTime Timemark { get; set; }

    public int Sequence {  get; set; }

    public int Status { get; set; }

    public int Entity {  get; set; }

    public string Vin { get; set; } = null!;

    public string? Motor { get; set; } = null!;

    public string Economic { get; set; } = null!;

    public int Manufacturer { get; set; }

    public int? CarrierH { get; set; }

    public int? Situation { get; set; }

    public int? MaintenanceH { get; set; }

    public int? InsuranceH { get; set; }

    public virtual CarrierH? CarrierHNavigation { get; set; }

    public virtual Status? StatusNavigation { get; set; }

    public virtual Truck? TruckNavigation { get; set; }

    public virtual Situation? SituationNavigation { get; set; }

    public virtual Manufacturer? ManufacturerNavigation { get; set; }

    public virtual InsuranceH? InsuranceHNavigation { get; set; }

    public virtual MaintenanceH? MaintenanceHNavigation { get; set; }



    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container) {
        UniqueValidator Unique = new();
        PointerValidator Pointer = new(true);
        Container = [
                .. Container,
            (nameof(Vin), [Unique, new LengthValidator(17, 17)]),
            (nameof(Economic), [new LengthValidator(1, 16)]),
            (nameof(Sequence), [new RequiredValidator()]),
            (nameof(Manufacturer), [Pointer]),
            //(nameof(CarrierH), [Pointer]),
            (nameof(Status), [Pointer]),
            (nameof(Entity), [Pointer])
        ];

        return Container;
    }

    public static void Set(ModelBuilder builder) {
        _ = builder.Entity<TruckH>(entity => {
            _ = entity.HasKey(e => e.Id);
            _ = entity.Property(e => e.Id)
                .HasColumnName("id");
            _ = entity.ToTable("Trucks_H");

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

            _ = entity.Property(e => e.MaintenanceH)
               .HasColumnName("MaintenanceH");
            _ = entity.Property(e => e.InsuranceH)
               .HasColumnName("InsuranceH");
            _ = entity.Property(e => e.CarrierH)
              .HasColumnName("CarrierH");

            _ = entity.HasOne(d => d.StatusNavigation)
               .WithMany(p => p.TrucksH)
               .HasForeignKey(d => d.Status);

            _ = entity.HasOne(d => d.TruckNavigation)
               .WithMany(p => p.TrucksH)
               .HasForeignKey(d => d.Entity);

          _ = entity.HasOne(d => d.CarrierHNavigation)
               .WithMany(p => p.TrucksH)
               .HasForeignKey(d => d.CarrierH);

            _ = entity.HasOne(d => d.ManufacturerNavigation)
                .WithMany(p => p.TrucksH)
                .HasForeignKey(d => d.Manufacturer)
                .OnDelete(DeleteBehavior.ClientSetNull);
            
            _ = entity.HasOne(d => d.SituationNavigation)
                .WithMany(p => p.TrucksH)
                .HasForeignKey(d => d.Situation)
                .OnDelete(DeleteBehavior.ClientSetNull);

            _ = entity.HasOne(d => d.InsuranceHNavigation)
                .WithMany(p => p.TrucksH)
                .HasForeignKey(d => d.InsuranceH);

            _ = entity.HasOne(d => d.MaintenanceHNavigation)
                .WithMany(p => p.TrucksH)
                .HasForeignKey(d => d.MaintenanceH);

        });
    }
}