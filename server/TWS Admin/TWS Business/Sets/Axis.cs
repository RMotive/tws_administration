using CSM_Foundation.Source.Bases;
using CSM_Foundation.Source.Interfaces;
using CSM_Foundation.Source.Validators;

using Microsoft.EntityFrameworkCore;

namespace TWS_Business.Sets;

public partial class Axis
    : BSourceSet {
    public override int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Quantity { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<TrailerClass> TrailerClasses { get; set; } = [];

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container) {
        RequiredValidator Required = new();

        Container = [
                .. Container,
            (nameof(Name), [Required, new LengthValidator(1, 30)]),
        ];

        return Container;
    }

    public static void Set(ModelBuilder builder) {
        _ = builder.Entity<Axis>(entity => {
            _ = entity.HasKey(e => e.Id);
            _ = entity.ToTable("Axes");

            _ = entity.Property(e => e.Id)
                 .HasColumnName("id");

            _ = entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);

            _ = entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);

        });
    }
}
