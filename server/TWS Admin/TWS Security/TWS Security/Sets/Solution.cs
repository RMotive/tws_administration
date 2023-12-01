using System;
using System.Collections.Generic;

namespace TWS_Security.Models;

public partial class Solution
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Sign { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Permit> Permits { get; set; } = new List<Permit>();
}
