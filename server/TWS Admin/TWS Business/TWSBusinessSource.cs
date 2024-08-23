using CSM_Foundation.Source.Bases;
using CSM_Foundation.Source.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using TWS_Business.Sets;

namespace TWS_Business;

public partial class TWSBusinessSource : BSource<TWSBusinessSource> {
    public TWSBusinessSource(DbContextOptions<TWSBusinessSource> options)
        : base(options) {
    }

    public TWSBusinessSource()
        : base() {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseSqlServer("Server=DESKTOP-M2SPTNQ;Database=TWS Business; Trusted_Connection=True; Encrypt=False");

        optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddDebug()))
                             .EnableSensitiveDataLogging();
    }
    public virtual DbSet<Insurance> Insurances { get; set; }

    public virtual DbSet<Maintenance> Maintenances { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Plate> Plates { get; set; }

    public virtual DbSet<Sct> Scts { get; set; }

    public virtual DbSet<Situation> Situations { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<TruckH> HPTrucks { get; set; }

    public virtual DbSet<Truck> Trucks { get; set; }

    public virtual DbSet<Carrier> Carriers { get; set; }

    public virtual DbSet<Approach> Approaches { get; set; }

    public virtual DbSet<Usdot> Usdots { get; set; }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Trailer> Trailers { get; set; }

    public virtual DbSet<TrailerCommon> TrailersCommons { get; set; }

    public virtual DbSet<TrailerExternal> TrailersExternals { get; set; }

    public virtual DbSet<Identification> Identifications { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<DriverCommon> DriversCommons { get; set; }

    public virtual DbSet<DriverExternal> DriverExternals { get; set; }

    public virtual DbSet<Axis> Axes { get; set; }

    public virtual DbSet<TrailerClass> TrailerClasses { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<LoadType> LoadTypes { get; set; }

    public virtual DbSet<Section> Sections { get; set; }

    public virtual DbSet<YardLog> YardLogs { get; set; }



    protected override void OnModelCreating(ModelBuilder builder) {
        Sct.Set(builder);
        Plate.Set(builder);
        Truck.Set(builder);
        Situation.Set(builder);
        Insurance.Set(builder);
        Maintenance.Set(builder);
        Manufacturer.Set(builder);
        Trailer.Set(builder);
        TrailerClass.Set(builder);
        TrailerCommon.Set(builder);
        TrailerExternal.Set(builder);
        Axis.Set(builder);
        Status.Set(builder);
        Address.Set(builder);
        Approach.Set(builder);
        Carrier.Set(builder);
        Location.Set(builder);
        TruckExternal.Set(builder);
        TruckCommon.Set(builder);
        Identification.Set(builder);
        Employee.Set(builder);
        Driver.Set(builder);
        DriverCommon.Set(builder);
        DriverExternal.Set(builder);
        LoadType.Set(builder);
        Section.Set(builder);
        YardLog.Set(builder);
        TruckH.Set(builder);
        Usdot.Set(builder); 
        UsdotH.Set(builder);
        ApproachesH.Set(builder);
        CarrierH.Set(builder);

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
            new TruckCommon(),
            new TruckExternal(),
            new Sct(),
            new Status(),
            new Carrier(),
            new Usdot(),
            new Address(),
            new Trailer(),
            new TrailerClass(),
            new TrailerCommon(),
            new TrailerExternal(),
            new Location(),
            new Axis(),
            new Identification(),
            new Employee(),
            new Driver(),
            new DriverCommon(),
            new DriverExternal(),
            new LoadType(),
            new Section(),
            new YardLog(),
            new UsdotH(),
            new Approach(),
            new ApproachesH(),
            new TruckH()
        ];
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
