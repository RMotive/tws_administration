using System;
using System.Collections.Generic;

namespace TWS_Business.Sets;

public partial class Maintenance
{
    public override int Id { get; set; }

    public DateOnly Anual { get; set; }

    public DateOnly Trimestral { get; set; }

    public virtual Truck? Truck { get; set; }
}
