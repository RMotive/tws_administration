using System;
using System.Collections.Generic;

namespace TWS_Security.Models;

public partial class ProfilesPermit
{
    public int Permit { get; set; }

    public int Profile { get; set; }

    public virtual Permit PermitNavigation { get; set; } = null!;

    public virtual Profile ProfileNavigation { get; set; } = null!;
}
