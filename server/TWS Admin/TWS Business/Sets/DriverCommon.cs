using CSM_Foundation.Source.Bases;
using CSM_Foundation.Source.Interfaces;
using CSM_Foundation.Source.Validators;

using Microsoft.EntityFrameworkCore;

namespace TWS_Business.Sets;

public partial class DriverCommon
    : BSourceSet {
    public override int Id { get; set; }

    public string License { get; set; } = null!;

    public int Situation { get; set; }

    public virtual Situation? SituationNavigation { get; set; }

    public virtual ICollection<Driver> Drivers { get; set; } = [];

    public virtual ICollection<DriverExternal> DriversExternals { get; set; } = [];

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container) {
        RequiredValidator Required = new();

        Container = [
                .. Container,
            (nameof(License), [Required, new LengthValidator(8,12)]),
            (nameof(Situation), [new PointerValidator(true)]),
        ];

        return Container;
    }

    public static void Set(ModelBuilder builder) {
        _ = builder.Entity<DriverCommon>(entity => {
            _ = entity.HasKey(e => e.Id);
            _ = entity.ToTable("Drivers_Commons");

            _ = entity.Property(e => e.Id)
                 .HasColumnName("id");

            _ = entity.Property(e => e.License)
                .HasMaxLength(12)
                .IsUnicode(false);

            _ = entity.HasOne(d => d.SituationNavigation)
                .WithMany(p => p.DriversCommons)
                .HasForeignKey(d => d.Situation)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });
    }
}
