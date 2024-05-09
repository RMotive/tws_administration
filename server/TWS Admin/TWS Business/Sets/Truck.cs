using System;
using System.Collections.Generic;

namespace TWS_Business.Sets;

public partial class Truck
{
    public override int Id { get; set; }

    public string Vin { get; set; } = null!;

    public int Manufacturer { get; set; }

    public string Motor { get; set; } = null!;

    public int? Sct { get; set; }

    public int? Maintenance { get; set; }

    public int? Situation { get; set; }

    public int? Insurance { get; set; }

    public virtual Insurance? InsuranceNavigation { get; set; }

    public virtual Maintenance? MaintenanceNavigation { get; set; }

    public virtual Manufacturer ManufacturerNavigation { get; set; } = null!;

    public virtual ICollection<Plate> Plates { get; set; } = new List<Plate>();

    public virtual Sct? SctNavigation { get; set; }

    public virtual Situation? SituationNavigation { get; set; }
}
