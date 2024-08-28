using CSM_Foundation.Source.Bases;
using CSM_Foundation.Source.Interfaces;
using CSM_Foundation.Source.Validators;

using Microsoft.EntityFrameworkCore;

namespace TWS_Business.Sets;

public partial class Maintenance
    : BSourceSet {
    public override int Id { get; set; }
    public int Status { get; set; }

    public DateOnly Anual { get; set; }

    public DateOnly Trimestral { get; set; }

    public virtual Status? StatusNavigation { get; set; }

    public virtual ICollection<Trailer> Trailers { get; set; } = [];

    public virtual ICollection<Truck> Trucks { get; set; } = [];

    public virtual ICollection<MaintenanceH> MaintenancesH { get; set; } = [];


    public static void Set(ModelBuilder builder) {
        _ = builder.Entity<Maintenance>(entity => {
            _ = entity.HasKey(e => e.Id);

            _ = entity.Property(e => e.Id)
                .HasColumnName("id");

            _ = entity.HasOne(d => d.StatusNavigation)
                .WithMany(p => p.Maintenances)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });
    }

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container) {
        RequiredValidator Required = new();

        Container = [
            ..Container,
            (nameof(Anual), [Required]),
            (nameof(Trimestral), [Required]),
            (nameof(Status), [Required, new PointerValidator(true)]),
        ];
        return Container;
    }
}
