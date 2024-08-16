using CSM_Foundation.Source.Bases;
using CSM_Foundation.Source.Interfaces;
using CSM_Foundation.Source.Validators;

using Microsoft.EntityFrameworkCore;

namespace TWS_Business.Sets;

public partial class Insurance
    : BSourceSet {
    public override int Id { get; set; }

    public int Status { get; set; }

    public string Policy { get; set; } = null!;

    public DateOnly Expiration { get; set; }

    public string Country { get; set; } = null!;

    public virtual Status? StatusNavigation { get; set; }

    public virtual ICollection<Truck> Trucks { get; set; } = [];

    public virtual ICollection<InsuranceH> InsurancesH { get; set; } = [];

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container) {
        RequiredValidator Required = new();

        Container = [
                .. Container,
            (nameof(Policy), [new UniqueValidator(), new LengthValidator(1, 20),]),
            (nameof(Expiration), [Required, new UniqueValidator()]),
            (nameof(Country), [new LengthValidator(2, 3)]),
            (nameof(Status), [Required, new PointerValidator(true)]),
        ];

        return Container;
    }

    public static void Set(ModelBuilder builder) {
        _ = builder.Entity<Insurance>(entity => {
            _ = entity.HasKey(e => e.Id);

            _ = entity.Property(e => e.Id)
                .HasColumnName("id");

            _ = entity.Property(e => e.Country)
                .HasMaxLength(3)
                .IsUnicode(false);

            _ = entity.Property(e => e.Policy)
                .HasMaxLength(20)
                .IsUnicode(false);

            _ = entity.HasOne(d => d.StatusNavigation)
                .WithMany(p => p.Insurances)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });
    }
}
