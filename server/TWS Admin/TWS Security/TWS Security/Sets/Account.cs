using System;
using System.Collections.Generic;

namespace TWS_Security.Models;

public partial class Account
{
    public int Id { get; set; }

    public string User { get; set; } = null!;

    public byte[] Password { get; set; } = null!;
}
