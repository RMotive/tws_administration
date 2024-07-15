namespace TWS_Security.Sets;

public partial class Contact {
    public override int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public virtual Account? Account { get; set; }
}
