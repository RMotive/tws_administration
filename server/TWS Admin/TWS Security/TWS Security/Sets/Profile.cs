using System;
using System.Collections.Generic;

namespace TWS_Security.Models;

public partial class Profile
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }
}
