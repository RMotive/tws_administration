using System;
using System.Collections.Generic;

namespace TWS_Business.Sets;

public partial class Situation
{
    public override int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Truck> Trucks { get; set; } = new List<Truck>();
}
