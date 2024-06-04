namespace TWS_Business.Sets;

public partial class Manufacturer
{
    public override int Id { get; set; }

    public string Model { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public DateOnly Year { get; set; }


}
