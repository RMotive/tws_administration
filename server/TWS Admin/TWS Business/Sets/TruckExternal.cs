using CSM_Foundation.Source.Bases;
using CSM_Foundation.Source.Interfaces;
using CSM_Foundation.Source.Validators;

using Microsoft.EntityFrameworkCore;

namespace TWS_Business.Sets;

public partial class TruckExternal
    : BSourceSet {
    public override int Id { get; set; }

    public int Status { get; set; }

    public int Common { get; set; }

    public virtual Status? StatusNavigation { get; set; }

    public virtual TruckCommon? TruckCommonNavigation { get; set; }

    public virtual ICollection<YardLog> YardLogs { get; set; } = [];

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container) {
        RequiredValidator Required = new();

        Container = [
                .. Container,
            (nameof(Common), [Required, new PointerValidator(true)]),
            (nameof(Status), [Required, new PointerValidator(true)]),
        ];

        return Container;
    }

    public static void Set(ModelBuilder builder) {
        _ = builder.Entity<TruckExternal>(entity => {
            _ = entity.HasKey(e => e.Id);
            _ = entity.ToTable("Trucks_Externals");

            _ = entity.Property(e => e.Id)
                 .HasColumnName("id");

            _ = entity.HasOne(d => d.TruckCommonNavigation)
               .WithMany(p => p.TrucksExternals)
               .HasForeignKey(d => d.Common);

            _ = entity.HasOne(d => d.StatusNavigation)
                .WithMany(p => p.TrucksExternals)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });
    }
}
