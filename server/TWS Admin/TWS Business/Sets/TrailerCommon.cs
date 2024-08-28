using CSM_Foundation.Source.Bases;
using CSM_Foundation.Source.Interfaces;
using CSM_Foundation.Source.Validators;

using Microsoft.EntityFrameworkCore;

namespace TWS_Business.Sets;

public partial class TrailerCommon
    : BSourceSet {
    public override int Id { get; set; }

    public int Status { get; set; }

    public string Economic { get; set; } = null!;

    public int Class { get; set; }

    public int Carrier { get; set; }

    public int Situation { get; set; }

    public int? Location { get; set; }

    public virtual Status? StatusNavigation { get; set; }

    public virtual TrailerClass? TrailerClassNavigation { get; set; }

    public virtual Carrier? CarrierNavigation { get; set; }

    public virtual Situation? SituationNavigation { get; set; }

    public virtual Location? LocationNavigation { get; set; }

    public virtual ICollection<Trailer> Trailers { get; set; } = [];

    public virtual ICollection<TrailerExternal> TrailersExternals { get; set; } = [];

    public virtual ICollection<Plate> Plates { get; set; } = [];


    protected override (string Property, IValidator[])[] Validations((string Property, IValidator[])[] Container) {
        RequiredValidator Required = new();

        Container = [
                .. Container,
            (nameof(Class), [Required, new PointerValidator(true)]),
            (nameof(Economic), [Required, new LengthValidator(1, 16)]),
            (nameof(Carrier), [Required, new PointerValidator(true)]),
            (nameof(Situation), [Required, new PointerValidator(true)]),
            (nameof(Status), [Required, new PointerValidator(true)]),
        ];

        return Container;
    }

    public static void Set(ModelBuilder builder) {
        _ = builder.Entity<TrailerCommon>(entity => {
            _ = entity.HasKey(e => e.Id);
            _ = entity.ToTable("Trailers_Commons");

            _ = entity.Property(e => e.Id)
                 .HasColumnName("id");

            _ = entity.Property(e => e.Economic)
                .HasMaxLength(16)
                .IsUnicode(false);

            _ = entity.HasOne(d => d.TrailerClassNavigation)
              .WithMany(p => p.TrailersCommons)
              .HasForeignKey(d => d.Class);

            _ = entity.HasOne(d => d.CarrierNavigation)
                .WithMany(p => p.TrailersCommons)
                .HasForeignKey(d => d.Carrier)
                .OnDelete(DeleteBehavior.ClientSetNull);
            
            _ = entity.HasOne(d => d.SituationNavigation)
               .WithMany(p => p.TrailersCommons)
               .HasForeignKey(d => d.Situation);

            _ = entity.HasOne(d => d.LocationNavigation)
               .WithMany(p => p.TrailersCommons)
               .HasForeignKey(d => d.Location)
               .OnDelete(DeleteBehavior.ClientSetNull);

            _ = entity.HasOne(d => d.StatusNavigation)
               .WithMany(p => p.TrailersCommons)
               .HasForeignKey(d => d.Status)
               .OnDelete(DeleteBehavior.ClientSetNull);
        });
    }
}
