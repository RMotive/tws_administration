using System;
using System.Collections.Generic;

namespace TWS_Business.Sets;

public partial class Sct
{
    public override int Id { get; set; }

    public string Type { get; set; } = null!;

    public string Number { get; set; } = null!;

    public string Configuration { get; set; } = null!;

    public virtual ICollection<Truck> Trucks { get; set; } = new List<Truck>();
}
