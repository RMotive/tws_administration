using CSM_Foundation.Core.Bases;
using CSM_Foundation.Source.Bases;
using CSM_Foundation.Source.Interfaces;
using CSM_Foundation.Source.Validators;

using Microsoft.EntityFrameworkCore;

namespace TWS_Business.Sets;

public partial class CarrierH
    : BHistorySourceSet {
    public override int Id { get; set; }

    public override DateTime Timemark { get; set; }

    public int Sequence { get; set; }

    public int Status { get; set; }

    public int Entity { get; set; }

    public string Name { get; set; } = null!;

    public int? ContactH { get; set; }

    public int Address { get; set; }

    public int? UsdotH { get; set; }

    public int? SctH { get; set; }

    public virtual Carrier? CarrierNavigation { get; set; }

    public virtual Status? StatusNavigation { get; set; }

    public virtual ContactH? ContactHNavigation { get; set; }

    public virtual Address? AddressNavigation { get; set; }

    public virtual UsdotH? UsdotHNavigation { get; set; }

    public virtual SctH? SctHNavigation { get; set; }

    public virtual ICollection<TruckH> TrucksH { get; set; } = [];

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container) {
        RequiredValidator Required = new();

        Container = [
                .. Container,
            (nameof(Name), [Required, new LengthValidator(1, 20)]),
            //(nameof(ContactH), [Required, new PointerValidator(true)]),
            (nameof(Address), [Required, new PointerValidator(true)]),
            (nameof(Status), [Required, new PointerValidator(true)]),
            (nameof(Entity), [Required, new PointerValidator(true)]),

        ];

        return Container;
    }

    public static void Set(ModelBuilder builder) {
        _ = builder.Entity<CarrierH>(entity => {
            _ = entity.HasKey(e => e.Id);
            _ = entity.Property(e => e.Id)
               .HasColumnName("id");
            _ = entity.ToTable("Carriers_H");

            _ = entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);

            _ = entity.Property(e => e.ContactH)
                .HasColumnName("ContactH");

            _ = entity.HasOne(d => d.ContactHNavigation)
              .WithMany(p => p.CarriersH)
              .HasForeignKey(d => d.ContactH);

            _ = entity.HasOne(d => d.CarrierNavigation)
              .WithMany(p => p.CarriersH)
              .HasForeignKey(d => d.Entity);

            _ = entity.HasOne(d => d.AddressNavigation)
                .WithMany(p => p.CarriersH)
                .HasForeignKey(d => d.Address)
                .OnDelete(DeleteBehavior.ClientSetNull);

            _ = entity.Property(e => e.UsdotH)
                .HasColumnName("USDOTH");

            _ = entity.HasOne(d => d.UsdotHNavigation)
               .WithMany(p => p.CarriersH)
               .HasForeignKey(d => d.UsdotH);

            _ = entity.Property(e => e.SctH)
                .HasColumnName("SCTH");

            _ = entity.HasOne(d => d.SctHNavigation)
               .WithMany(p => p.CarriersH)
               .HasForeignKey(d => d.SctH);

            _ = entity.HasOne(d => d.StatusNavigation)
                .WithMany(p => p.CarriersH)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });
    }
}
