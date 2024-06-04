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


}
