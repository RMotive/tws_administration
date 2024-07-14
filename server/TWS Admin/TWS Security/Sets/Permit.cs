namespace TWS_Security.Sets;

public partial class Permit {
    public override int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int Solution { get; set; }

    public string Reference { get; set; } = null!;
}
