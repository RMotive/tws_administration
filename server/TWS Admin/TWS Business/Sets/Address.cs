using CSM_Foundation.Source.Bases;
using CSM_Foundation.Source.Interfaces;
using CSM_Foundation.Source.Validators;

using Microsoft.EntityFrameworkCore;

namespace TWS_Business.Sets;

public partial class Address
    : BSourceSet {
    public override int Id { get; set; }

    public string? State { get; set; }

    public string? Street { get; set; }

    public string? AltStreet { get; set; }

    public string? City { get; set; }

    public string? Zip { get; set; }

    public string Country { get; set; } = null!;

    public string? Colonia { get; set; }

    public virtual ICollection<Carrier> Carriers { get; set; } = [];

    public virtual ICollection<CarrierH> CarriersH { get; set; } = [];

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container) {

        Container = [
                .. Container,
            (nameof(Country), [ new RequiredValidator(),new LengthValidator(1, 3)]),
        ];

        return Container;
    }

    public static void Set(ModelBuilder builder) {
        _ = builder.Entity<Address>(entity => {
            _ = entity.HasKey(e => e.Id);
            _ = entity.ToTable("Addresses");

            _ = entity.Property(e => e.Id)
                 .HasColumnName("id");

            _ = entity.Property(e => e.Street)
                .HasMaxLength(100)
                .IsUnicode(false);

            _ = entity.Property(e => e.AltStreet)
                .HasMaxLength(100)
                .IsUnicode(false);

            _ = entity.Property(e => e.City)
                .HasMaxLength(30)
                .IsUnicode(false);

            _ = entity.Property(e => e.Zip)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ZIP");

            _ = entity.Property(e => e.Country)
                .HasMaxLength(3)
                .IsUnicode(false);

            _ = entity.Property(e => e.Colonia)
                .HasMaxLength(30)
                .IsUnicode(false);
        });
    }
}
