

using CSM_Foundation.Source.Bases;
using CSM_Foundation.Source.Interfaces;
using CSM_Foundation.Source.Validators;
using Microsoft.EntityFrameworkCore;

namespace TWS_Business.Sets;
public partial class HPTruck
: BSourceSet {
    public override int Id { get; set; }

    public DateTime Creation { get; set; }

    public string Vin { get; set; } = null!;

    public string Motor { get; set; } = null!;

    public int Status { get; set; }

    public virtual Status? StatusNavigation { get; set; }

    public virtual ICollection<Truck> Trucks { get; set; } = [];

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container) {
        UniqueValidator Unique = new();

        Container = [
                .. Container,
            (nameof(Vin), [Unique, new LengthValidator(17, 17)]),
            (nameof(Motor), [Unique, new LengthValidator(15, 16)]),
            (nameof(Status), [new PointerValidator(true)]),


        ];

        return Container;
    }

    public static void Set(ModelBuilder builder) {
        _ = builder.Entity<HPTruck>(entity => {
            _ = entity.HasKey(e => e.Id);
            _ = entity.ToTable("HP_Trucks");
            _ = entity.Property(e => e.Id)
               .HasColumnName("id");

            _ = entity.HasOne(d => d.StatusNavigation)
                .WithMany(p => p.HPTrucks)
                .HasForeignKey(d => d.Status);
            _ = entity.HasIndex(e => e.Vin)
                .IsUnique();
            _ = entity.Property(e => e.Vin)
                .HasMaxLength(17)
                .IsUnicode(false)
                .HasColumnName("VIN");

            _ = entity.HasIndex(e => e.Motor)
                .IsUnique();
            _ = entity.Property(e => e.Motor)
               .HasMaxLength(16)
               .IsUnicode(false);

        });
    }
}