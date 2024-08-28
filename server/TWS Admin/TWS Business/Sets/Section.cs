using CSM_Foundation.Source.Bases;
using CSM_Foundation.Source.Interfaces;
using CSM_Foundation.Source.Validators;

using Microsoft.EntityFrameworkCore;

namespace TWS_Business.Sets;

public partial class Section
    : BSourceSet {
    public override int Id { get; set; }

    public int Status { get; set; }

    public string Name { get; set; } = null!;

    public int Yard { get; set; }

    public int Capacity { get; set; }

    public int Ocupancy { get; set; }


    public virtual Status? StatusNavigation { get; set; }

    public virtual Location? LocationNavigation { get; set; }

    public virtual ICollection<YardLog> YardLogs { get; set; } = [];

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container) {
        RequiredValidator Required = new();

        Container = [
                .. Container,
            (nameof(Status), [new PointerValidator(true)]),
            (nameof(Yard), [new PointerValidator(true)]),
            (nameof(Name), [new LengthValidator(1,32)]),
        ];

        return Container;
    }

    public static void Set(ModelBuilder builder) {
        _ = builder.Entity<Section>(entity => {
            _ = entity.HasKey(e => e.Id);
            _ = entity.ToTable("Sections");

            _ = entity.Property(e => e.Id)
                 .HasColumnName("id");

            _ = entity.Property(e => e.Name)
                .HasMaxLength(32)
                .IsUnicode(false);

            _ = entity.HasOne(d => d.LocationNavigation)
                .WithMany(p => p.Sections)
                .HasForeignKey(d => d.Yard);

            _ = entity.HasOne(d => d.StatusNavigation)
                .WithMany(p => p.Sections)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });
    }
}
