namespace TWS_Security.Sets;

public partial class Account {
    public int Id { get; set; }

    public string User { get; set; } = null!;

    public byte[] Password { get; set; } = null!;

    public bool Wildcard { get; set; }
}
