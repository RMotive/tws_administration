using CSM_Foundation.Source.Bases;
using CSM_Foundation.Source.Interfaces;
using CSM_Foundation.Source.Validators;

using Microsoft.EntityFrameworkCore;

namespace TWS_Business.Sets;

public partial class TruckCommon
    : BSourceSet {
    public override int Id { get; set; }

    public int Status { get; set; }

    public string Vin { get; set; } = null!;

    public string Economic { get; set; } = null!;

    public int Carrier { get; set; }

    public int? Location { get; set; }

    public int? Situation { get; set; }

    public virtual Carrier? CarrierNavigation { get; set; }

    public virtual Situation? SituationNavigation { get; set; }

    public virtual Location? LocationNavigation { get; set; }

    public virtual Status? StatusNavigation { get; set; }

    public virtual ICollection<Truck> Trucks { get; set; } = [];

    public virtual ICollection<TruckExternal> TrucksExternals { get; set; } = [];

    public virtual ICollection<Plate> Plates { get; set; } = [];


    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container) {
        RequiredValidator Required = new();
        UniqueValidator Unique = new();

        Container = [
                .. Container,
            (nameof(Vin), [Unique, new LengthValidator(17, 17)]),
            (nameof(Economic), [Required, new LengthValidator(1, 16)]),
            (nameof(Carrier), [new PointerValidator(true)]),
            (nameof(Status), [new PointerValidator(true)])
        ];

        return Container;
    }

    public static void Set(ModelBuilder builder) {
        _ = builder.Entity<TruckCommon>(entity => {
            _ = entity.HasKey(e => e.Id);
            _ = entity.ToTable("Trucks_Commons");

            _ = entity.Property(e => e.Id)
                 .HasColumnName("id");

            _ = entity.Property(e => e.Vin)
               .HasMaxLength(17)
               .IsUnicode(false)
               .HasColumnName("VIN");

            _ = entity.Property(e => e.Economic)
                .HasMaxLength(16)
                .IsUnicode(false);

            _ = entity.HasOne(d => d.CarrierNavigation)
              .WithMany(p => p.TrucksCommons)
              .HasForeignKey(d => d.Carrier);
            
            _ = entity.HasOne(d => d.SituationNavigation)
               .WithMany(p => p.TrucksCommons)
               .HasForeignKey(d => d.Situation);

            _ = entity.HasOne(d => d.LocationNavigation)
               .WithMany(p => p.TrucksCommons)
               .HasForeignKey(d => d.Location)
               .OnDelete(DeleteBehavior.ClientSetNull);

            _ = entity.HasOne(d => d.StatusNavigation)
               .WithMany(p => p.TrucksCommons)
               .HasForeignKey(d => d.Status)
               .OnDelete(DeleteBehavior.ClientSetNull);
        });
    }
}
