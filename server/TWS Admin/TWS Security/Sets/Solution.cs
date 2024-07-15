namespace TWS_Security.Sets;

public partial class Solution {
    public override int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Sign { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Permit> Permits { get; set; } = [];
}
