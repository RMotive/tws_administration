namespace TWS_Business.Sets;

public partial class Insurance
{
    public override int Id { get; set; }

    public string Policy { get; set; } = null!;

    public DateOnly Expiration { get; set; }

    public string Country { get; set; } = null!;

    public virtual ICollection<Truck> Trucks { get; set; } = [];
}
