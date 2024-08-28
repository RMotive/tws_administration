using CSM_Foundation.Source.Bases;
using CSM_Foundation.Source.Interfaces;
using CSM_Foundation.Source.Validators;

using Microsoft.EntityFrameworkCore;

namespace TWS_Business.Sets;

public partial class Trailer
    : BSourceSet {
    public override int Id { get; set; }

    public int Status { get; set; }

    public int Common { get; set; }

    public int Manufacturer { get; set; }

    public int? Maintenance { get; set; }

    public virtual Status? StatusNavigation { get; set; }

    public virtual TrailerCommon? TrailerCommonNavigation { get; set; }

    public virtual Manufacturer? ManufacturerNavigation { get; set; }

    public virtual Maintenance? MaintenanceNavigation { get; set; }

    public virtual ICollection<YardLog> YardLogs { get; set; } = [];

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container) {
        RequiredValidator Required = new();
        PointerValidator pointer = new(true);
        Container = [
                .. Container,
            (nameof(Manufacturer), [Required, pointer]),
            (nameof(Common), [Required, pointer]),
            (nameof(Status), [Required, pointer]),
        ];

        return Container;
    }

    public static void Set(ModelBuilder builder) {
        _ = builder.Entity<Trailer>(entity => {
            _ = entity.HasKey(e => e.Id);
            _ = entity.ToTable("Trailers");

            _ = entity.Property(e => e.Id)
                 .HasColumnName("id");

            _ = entity.HasOne(d => d.TrailerCommonNavigation)
                .WithMany(p => p.Trailers)
                .HasForeignKey(d => d.Common);

            _ = entity.HasOne(d => d.ManufacturerNavigation)
                .WithMany(p => p.Trailers)
                .HasForeignKey(d => d.Manufacturer)
                .OnDelete(DeleteBehavior.ClientSetNull);

            _ = entity.HasOne(d => d.MaintenanceNavigation)
                .WithMany(p => p.Trailers)
                .HasForeignKey(d => d.Maintenance);

            _ = entity.HasOne(d => d.StatusNavigation)
               .WithMany(p => p.Trailers)
               .HasForeignKey(d => d.Status)
               .OnDelete(DeleteBehavior.ClientSetNull);

        });
    }
}
