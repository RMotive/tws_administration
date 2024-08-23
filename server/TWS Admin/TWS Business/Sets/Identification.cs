using CSM_Foundation.Source.Bases;
using CSM_Foundation.Source.Interfaces;
using CSM_Foundation.Source.Validators;

using Microsoft.EntityFrameworkCore;

namespace TWS_Business.Sets;

public partial class Identification
    : BSourceSet {
    public override int Id { get; set; }

    public int Status { get; set; }

    public string Name { get; set; } = null!;

    public string FatherLastname { get; set; } = null!;

    public string MotherLastName { get; set; } = null!;
    public virtual Status? StatusNavigation { get; set; }

    public DateOnly Birthday { get; set; }

    public virtual ICollection<DriverExternal> DriversExternals { get; set; } = [];

    public virtual ICollection<Employee> Employees { get; set; } = [];


    public static void Set(ModelBuilder builder) {
        _ = builder.Entity<Identification>(entity => {
            _ = entity.HasKey(e => e.Id);
            _ = entity.Property(e => e.Id)
               .HasColumnName("id");
            _ = entity.ToTable("Identifications");

            _ = entity.Property(e => e.Name)
                .HasMaxLength(32)
                .IsUnicode(false);

            _ = entity.Property(e => e.FatherLastname)
                .HasMaxLength(32)
                .IsUnicode(false);

            _ = entity.Property(e => e.MotherLastName)
                .HasMaxLength(32)
                .IsUnicode(false);

            _ = entity.HasOne(d => d.StatusNavigation)
                .WithMany(p => p.Identifications)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });
    }

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container) {
        RequiredValidator Required = new();
        Container = [
            ..Container,
            (nameof(Name), [Required, new LengthValidator(1,32)]),
            (nameof(FatherLastname), [Required, new LengthValidator(1,32)]),
            (nameof(MotherLastName), [Required, new LengthValidator(1,32)]),
            (nameof(Status), [Required, new PointerValidator(true)])
        ];
        return Container;
    }
}
