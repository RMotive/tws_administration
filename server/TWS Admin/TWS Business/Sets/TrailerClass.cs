using CSM_Foundation.Source.Bases;
using CSM_Foundation.Source.Interfaces;
using CSM_Foundation.Source.Validators;

using Microsoft.EntityFrameworkCore;

namespace TWS_Business.Sets;

public partial class TrailerClass
    : BSourceSet {
    public override int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Axis { get; set; }

    public string? Description { get; set; }

    public virtual Axis? AxisNavigation { get; set; }

    public virtual ICollection<TrailerCommon> TrailersCommons { get; set; } = [];

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container) {
        RequiredValidator Required = new();

        Container = [
                .. Container,
            (nameof(Name), [Required, new LengthValidator(1, 30)]),
            (nameof(Axis), [new PointerValidator(true)]),
        ];

        return Container;
    }

    public static void Set(ModelBuilder builder) {
        _ = builder.Entity<TrailerClass>(entity => {
            _ = entity.HasKey(e => e.Id);
            _ = entity.ToTable("Trailer_Classes");

            _ = entity.Property(e => e.Id)
                 .HasColumnName("id");

            _ = entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);

            _ = entity.Property(e => e.Description)
                .HasMaxLength(30)
                .IsUnicode(false);

            _ = entity.HasOne(d => d.AxisNavigation)
                 .WithMany(p => p.TrailerClasses)
                 .HasForeignKey(d => d.Axis)
                 .OnDelete(DeleteBehavior.ClientSetNull);
        });
    }
}
