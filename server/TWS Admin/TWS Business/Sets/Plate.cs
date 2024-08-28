using CSM_Foundation.Source.Bases;
using CSM_Foundation.Source.Interfaces;
using CSM_Foundation.Source.Validators;

using Microsoft.EntityFrameworkCore;

namespace TWS_Business.Sets;

public partial class Plate
    : BSourceSet {
    public override int Id { get; set; }

    public int Status { get; set; }

    public string Identifier { get; set; } = null!;

    public string State { get; set; } = null!;

    public string Country { get; set; } = null!;

    public DateOnly Expiration { get; set; }

    public int? Truck { get; set; }

    public int? Trailer { get; set; }

    public virtual TruckCommon? TruckCommonNavigation { get; set; }

    public virtual TrailerCommon? TrailerCommonNavigation { get; set; }

    public virtual Status? StatusNavigation { get; set; }

    public virtual ICollection<PlateH> PlatesH { get; set; } = [];

    public static void Set(ModelBuilder builder) {
        _ = builder.Entity<Plate>(entity => {
            _ = entity.HasKey(e => e.Id);

            _ = entity.Property(e => e.Id)
                .HasColumnName("id");
            _ = entity.Property(e => e.Country)
                .HasMaxLength(3)
                .IsUnicode(false);
            _ = entity.Property(e => e.Identifier)
                .HasMaxLength(12)
                .IsUnicode(false);
            _ = entity.Property(e => e.State)
                .HasMaxLength(3)
                .IsUnicode(false);

            _ = entity.HasOne(d => d.TruckCommonNavigation)
                .WithMany(p => p.Plates)
                .HasForeignKey(d => d.Truck)
                .OnDelete(DeleteBehavior.ClientSetNull);

            _ = entity.HasOne(d => d.TrailerCommonNavigation)
                .WithMany(p => p.Plates)
                .HasForeignKey(d => d.Trailer)
                .OnDelete(DeleteBehavior.ClientSetNull);

            _ = entity.HasOne(d => d.StatusNavigation)
                .WithMany(p => p.Plates)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });
    }

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container) {
        RequiredValidator Required = new();
        Container = [
            ..Container,
            (nameof(Identifier), [new LengthValidator(8, 12)]),
            (nameof(State), [new LengthValidator(2, 3)]),
            (nameof(Country), [new LengthValidator(2, 3)]),
            (nameof(Expiration), [Required]),
            (nameof(Status), [Required, new PointerValidator(true)]),
        ];
        return Container;
    }
}
