using TWS_Security.Contracts.Bases;

namespace TWS_Security.Models;

public partial class Permit : BDatasourceSet
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int Solution { get; set; }

    public virtual Solution SolutionNavigation { get; set; } = null!;
}
