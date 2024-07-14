using CSM_Foundation.Source.Bases;
using CSM_Foundation.Source.Interfaces;

using Microsoft.EntityFrameworkCore;

using TWS_Business.Sets;

namespace TWS_Business;

public partial class TWSBusinessSource : BSource<TWSBusinessSource> {
    public TWSBusinessSource(DbContextOptions<TWSBusinessSource> options)
        : base(options) {
    }

    public TWSBusinessSource()
        : base() {
    }

    public virtual DbSet<Insurance> Insurances { get; set; }

    public virtual DbSet<Maintenance> Maintenances { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Plate> Plates { get; set; }

    public virtual DbSet<Sct> Scts { get; set; }

    public virtual DbSet<Situation> Situations { get; set; }

    public virtual DbSet<Truck> Trucks { get; set; }

    protected override void OnModelCreating(ModelBuilder builder) {
        Sct.Set(builder);
        Plate.Set(builder);
        Truck.Set(builder);
        Situation.Set(builder);
        Insurance.Set(builder);
        Maintenance.Set(builder);
        Manufacturer.Set(builder);

        OnModelCreatingPartial(builder);
    }

    protected override ISourceSet[] EvaluateFactory() {
        return [
            new Plate(),
            new Manufacturer(),
            new Maintenance(),
            new Insurance(),
            new Situation(),
            new Truck(),
            new Sct()
        ];
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
