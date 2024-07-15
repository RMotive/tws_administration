using CSM_Foundation.Source.Bases;
using CSM_Foundation.Source.Interfaces;
using CSM_Foundation.Source.Validators;

using Microsoft.EntityFrameworkCore;

namespace TWS_Business.Sets;

public partial class Manufacturer
    : BSourceSet {
    public override int Id { get; set; }

    public string Model { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public DateOnly Year { get; set; }

    public virtual ICollection<Truck> Trucks { get; set; } = [];

    public static void Set(ModelBuilder builder) {
        _ = builder.Entity<Manufacturer>(entity => {
            _ = entity.HasKey(e => e.Id);

            _ = entity.Property(e => e.Id)
                .HasColumnName("id");

            _ = entity.Property(e => e.Brand)
                .HasMaxLength(15)
                .IsUnicode(false);

            _ = entity.Property(e => e.Model)
                .HasMaxLength(30)
                .IsUnicode(false);
        });
    }

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container) {
        RequiredValidator Required = new();

        Container = [
                .. Container,
            (nameof(Model), [Required, new LengthValidator(1, 30)]),
            (nameof(Brand), [Required, new LengthValidator(1, 15)]),
            (nameof(Year), [Required]),
        ];

        return Container;
    }
}
