using CSM_Foundation.Source.Bases;
using CSM_Foundation.Source.Interfaces;
using CSM_Foundation.Source.Validators;

using Microsoft.EntityFrameworkCore;

namespace TWS_Business.Sets;
public partial class Status
: BSourceSet {
    public override int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Truck> Trucks { get; set; } = [];

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container) {
        RequiredValidator Required = new();

        Container = [
                .. Container,
            (nameof(Name), [Required, new LengthValidator(1, 25)]),
            (nameof(Description), [new LengthValidator(1, 150)]),
        ];

        return Container;
    }

    public static void Set(ModelBuilder builder) {
        _ = builder.Entity<Status>(entity => {
            _ = builder.Entity<Situation>(entity => {
                _ = entity.HasKey(e => e.Id);
                _ = entity.Property(e => e.Id)
                    .HasColumnName("id");

                _ = entity.HasIndex(e => e.Name)
                    .IsUnique();
                _ = entity.Property(e => e.Name)
                    .HasMaxLength(25);

                _ = entity.Property(e => e.Description)
                    .HasMaxLength(150);
                
            });

        });
    }
}