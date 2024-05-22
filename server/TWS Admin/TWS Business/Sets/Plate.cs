namespace TWS_Business.Sets;

public partial class Plate
{
    public override int Id { get; set; }

    public string Identifier { get; set; } = null!;

    public string State { get; set; } = null!;

    public string Country { get; set; } = null!;

    public DateOnly Expiration { get; set; }

    public int Truck { get; set; }
}
