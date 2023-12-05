using TWS_Security.Contracts.Bases;

namespace TWS_Security.Models;

public partial class Solution : BDatasourceSet
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Sign { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Permit> Permits { get; set; } = new List<Permit>();
}
