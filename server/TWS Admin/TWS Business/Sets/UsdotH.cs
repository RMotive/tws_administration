using System.Diagnostics.Metrics;

using CSM_Foundation.Core.Bases;
using CSM_Foundation.Source.Bases;
using CSM_Foundation.Source.Interfaces;
using CSM_Foundation.Source.Validators;

using Microsoft.EntityFrameworkCore;

namespace TWS_Business.Sets;

public partial class UsdotH
    : BHistorySourceSet {
    public override int Id { get; set; }

    public override DateTime Timemark { get; set; }

    public int Sequence { get; set; }

    public int Status { get; set; }

    public int Entity { get; set; }
    public string Mc { get; set; } = null!;

    public string Scac { get; set; } = null!;
    public virtual Usdot? UsdotNavigation { get; set; }

    public virtual Status? StatusNavigation { get; set; }

    public virtual ICollection<CarrierH> CarriersH { get; set; } = [];


    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container) {
        RequiredValidator Required = new();

        Container = [
                .. Container,
            (nameof(Mc), [Required, new LengthValidator(7, 7)]),
            (nameof(Scac), [Required, new LengthValidator(4, 4)]),
            (nameof(Status), [Required, new PointerValidator(true)]),
            (nameof(Entity), [Required, new PointerValidator(true)]),
        ];

        return Container;
    }

    public static void Set(ModelBuilder builder) {
        _ = builder.Entity<UsdotH>(entity => {
            _ = entity.HasKey(e => e.Id);
            _ = entity.Property(e => e.Id)
               .HasColumnName("id");
            _ = entity.ToTable("USDOT_H");

            _ = entity.Property(e => e.Mc)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("MC");

            _ = entity.Property(e => e.Scac)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("SCAC");

            _ = entity.HasOne(d => d.UsdotNavigation)
                .WithMany(p => p.UsdotsH)
                .HasForeignKey(d => d.Entity)
                .OnDelete(DeleteBehavior.ClientSetNull);

            _ = entity.HasOne(d => d.StatusNavigation)
                .WithMany(p => p.UsdotsH)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });
    }
}
