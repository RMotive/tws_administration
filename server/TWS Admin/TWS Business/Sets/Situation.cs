using CSM_Foundation.Source.Bases;
using CSM_Foundation.Source.Interfaces;
using CSM_Foundation.Source.Validators;

using Microsoft.EntityFrameworkCore;

namespace TWS_Business.Sets;

public partial class Situation
    : BSourceSet {
    public override int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Truck>? Trucks { get; set; } = [];

    public virtual ICollection<TruckH> TrucksH { get; set; } = [];

    public static void Set(ModelBuilder builder) {
        _ = builder.Entity<Situation>(entity => {
            _ = entity.HasKey(e => e.Id);

            _ = entity.HasIndex(e => e.Name)
                .IsUnique();

            _ = entity.Property(e => e.Id)
                .HasColumnName("id");
            _ = entity.Property(e => e.Description)
                .HasMaxLength(100);
            _ = entity.Property(e => e.Name)
                .HasMaxLength(25);
        });
    }

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container) {
        RequiredValidator Required = new();
        Container = [
            ..Container,
            (nameof(Name), [Required, new LengthValidator(1, 25)]),
        ];
        return Container;
    }
}
