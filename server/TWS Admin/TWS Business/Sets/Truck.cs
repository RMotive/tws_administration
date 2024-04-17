using System;
using System.Collections.Generic;

namespace TWS_Business.Sets;

public partial class Truck
{
    public override int Id { get; set; }

    public string Vin { get; set; } = null!;

    public int Plate { get; set; }

    public int Manufacturer { get; set; }

    public string Motor { get; set; } = null!;

    public string Sct { get; set; } = null!;

    public int Maintenance { get; set; }

    public int Situation { get; set; }

    public int Insurance { get; set; }

    public virtual Insurance InsuranceNavigation { get; set; } = null!;

    public virtual Maintenance MaintenanceNavigation { get; set; } = null!;

    public virtual Manufacturer ManufacturerNavigation { get; set; } = null!;

    public virtual Plate PlateNavigation { get; set; } = null!;

    public virtual Situation SituationNavigation { get; set; } = null!;
}
