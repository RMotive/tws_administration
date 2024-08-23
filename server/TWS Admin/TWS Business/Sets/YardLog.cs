using CSM_Foundation.Source.Bases;
using CSM_Foundation.Source.Interfaces;
using CSM_Foundation.Source.Validators;

using Microsoft.EntityFrameworkCore;

namespace TWS_Business.Sets;

public partial class YardLog
    : BSourceSet {
    public override int Id { get; set; }

    public bool Entry { get; set; }

    public int? Truck { get; set; }

    public int? TruckExternal { get; set; }

    public int? Trailer { get; set; }

    public int? TrailerExternal { get; set; }

    public int LoadType { get; set; }

    public int Section { get; set; }

    public int? Driver { get; set; }

    public int? DriverExternal { get; set; }

    public DateTime? Timestamp { get; set; }

    public int Guard { get; set; }

    public string Gname { get; set; } = null!;

    public string FromTo { get; set; } = null!;

    public bool Damage { get; set; }

    public string TTPicture { get; set; } = null!;

    public string? DmgEvidence { get; set; } = null!;

    public virtual Driver? DriverNavigation { get; set; }

    public virtual DriverExternal? DriverExternalNavigation { get; set; }

    public virtual Truck? TruckNavigation { get; set; }

    public virtual TruckExternal? TruckExternalNavigation { get; set; }

    public virtual Trailer? TrailerNavigation { get; set; }

    public virtual TrailerExternal? TrailerExternalNavigation { get; set; }

    public virtual LoadType? LoadTypeNavigation { get; set; }

    public virtual Section? SectionNavigation { get; set; }

    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container) {
        RequiredValidator Required = new();

        Container = [
                .. Container,
            (nameof(TTPicture), [new LengthValidator(1, 100)]),
            (nameof(Gname), [new LengthValidator(1, 100)]),
            (nameof(FromTo), [new LengthValidator(1, 100)]),
            (nameof(LoadType), [new PointerValidator(true)]),
            (nameof(Section), [new PointerValidator(true)]),
        ];

        return Container;
    }

    public static void Set(ModelBuilder builder) {
        _ = builder.Entity<YardLog>(entity => {
            _ = entity.HasKey(e => e.Id);
            _ = entity.ToTable("Yard_Logs");

            _ = entity.Property(e => e.Id)
                 .HasColumnName("id");

            _ = entity.Property(e => e.Gname)
                .HasMaxLength(100)
                .IsUnicode(false);

            _ = entity.Property(e => e.FromTo)
                .HasMaxLength(100)
                .IsUnicode(false);

            _ = entity.Property(e => e.TTPicture)
                .IsUnicode(false);

            _ = entity.Property(e => e.DmgEvidence)
               .IsUnicode(false);

            _ = entity.HasOne(d => d.DriverNavigation)
                .WithMany(p => p.YardLogs)
                .HasForeignKey(d => d.Driver)
                .OnDelete(DeleteBehavior.ClientSetNull);

            _ = entity.HasOne(d => d.DriverExternalNavigation)
               .WithMany(p => p.YardLogs)
               .HasForeignKey(d => d.DriverExternal)
               .OnDelete(DeleteBehavior.ClientSetNull);

            _ = entity.HasOne(d => d.TruckNavigation)
               .WithMany(p => p.YardLogs)
               .HasForeignKey(d => d.Truck)
               .OnDelete(DeleteBehavior.ClientSetNull);

            _ = entity.HasOne(d => d.TruckExternalNavigation)
               .WithMany(p => p.YardLogs)
               .HasForeignKey(d => d.TruckExternal)
               .OnDelete(DeleteBehavior.ClientSetNull);

            _ = entity.HasOne(d => d.TrailerNavigation)
               .WithMany(p => p.YardLogs)
               .HasForeignKey(d => d.Trailer)
               .OnDelete(DeleteBehavior.ClientSetNull);

            _ = entity.HasOne(d => d.TrailerExternalNavigation)
               .WithMany(p => p.YardLogs)
               .HasForeignKey(d => d.TrailerExternal)
               .OnDelete(DeleteBehavior.ClientSetNull);

            _ = entity.HasOne(d => d.LoadTypeNavigation)
               .WithMany(p => p.YardLogs)
               .HasForeignKey(d => d.LoadType)
               .OnDelete(DeleteBehavior.ClientSetNull);

            _ = entity.HasOne(d => d.SectionNavigation)
               .WithMany(p => p.YardLogs)
               .HasForeignKey(d => d.Section)
               .OnDelete(DeleteBehavior.ClientSetNull);
        });
    }
}
