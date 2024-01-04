namespace TWS_Security.Sets;

public partial class AccountsPermit {
    public int Account { get; set; }

    public int Permit { get; set; }

    public virtual Account AccountNavigation { get; set; } = null!;

    public virtual Permit PermitNavigation { get; set; } = null!;
}
