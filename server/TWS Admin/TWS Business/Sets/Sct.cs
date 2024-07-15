using CSM_Foundation.Source.Bases;
using CSM_Foundation.Source.Interfaces;
using CSM_Foundation.Source.Validators;

using Microsoft.EntityFrameworkCore;

namespace TWS_Business.Sets;

public partial class Sct
    : BSourceSet {
    public override int Id { get; set; }

    public string Type { get; set; } = null!;

    public string Number { get; set; } = null!;

    public string Configuration { get; set; } = null!;

    public virtual ICollection<Truck> Trucks { get; set; } = [];

    public static void Set(ModelBuilder builder) {
        _ = builder.Entity<Sct>(entity => {
            _ = entity.HasKey(e => e.Id);

            _ = entity.ToTable("SCT");

            _ = entity.Property(e => e.Id)
                .HasColumnName("id");
            _ = entity.Property(e => e.Configuration)
                .HasMaxLength(10)
                .IsUnicode(false);
            _ = entity.Property(e => e.Number)
                .HasMaxLength(25)
                .IsUnicode(false);
            _ = entity.Property(e => e.Type)
                .HasMaxLength(6)
                .IsUnicode(false);
        });
    }

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container) {
        RequiredValidator Required = new();
        Container = [
            ..Container,
            (nameof(Type), [Required, new LengthValidator(6,6)]),
            (nameof(Number), [Required, new LengthValidator(25,25)]),
            (nameof(Configuration), [Required, new LengthValidator(6,10)]),
        ];
        return Container;
    }
}
