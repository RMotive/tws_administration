using CSM_Foundation.Core.Bases;
using CSM_Foundation.Source.Bases;
using CSM_Foundation.Source.Interfaces;
using CSM_Foundation.Source.Validators;

using Microsoft.EntityFrameworkCore;

namespace TWS_Business.Sets;

public partial class SctH
    : BHistorySourceSet {
    public override int Id { get; set; }

    public override DateTime Timemark { get; set; }

    public int Sequence { get; set; }

    public int Status { get; set; }

    public int Entity { get; set; }

    public string Type { get; set; } = null!;

    public string Number { get; set; } = null!;

    public string Configuration { get; set; } = null!;

    public virtual Sct? SctNavigation { get; set; }

    public virtual Status? StatusNavigation { get; set; }

    public virtual ICollection<CarrierH> CarriersH { get; set; } = [];



    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container) {
        RequiredValidator Required = new();

        Container = [
                .. Container,
            (nameof(Type), [Required, new LengthValidator(6,6)]),
            (nameof(Number), [Required, new LengthValidator(25,25)]),
            (nameof(Configuration), [Required, new LengthValidator(6,10)]),
            (nameof(Status), [Required, new PointerValidator(true)]),
            (nameof(Entity), [Required, new PointerValidator(true)]),
        ];

        return Container;
    }

    public static void Set(ModelBuilder builder) {
        _ = builder.Entity<SctH>(entity => {
            _ = entity.HasKey(e => e.Id);
            _ = entity.Property(e => e.Id)
               .HasColumnName("id");
            _ = entity.ToTable("SCT_H");


            _ = entity.Property(e => e.Configuration)
                .HasMaxLength(10)
                .IsUnicode(false);
            _ = entity.Property(e => e.Number)
                .HasMaxLength(25)
                .IsUnicode(false);
            _ = entity.Property(e => e.Type)
                .HasMaxLength(6)
                .IsUnicode(false);

            _ = entity.HasOne(d => d.StatusNavigation)
                .WithMany(p => p.SctsH)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull);

            _ = entity.HasOne(d => d.SctNavigation)
                .WithMany(p => p.SctsH)
                .HasForeignKey(d => d.Entity)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });
    }
}
