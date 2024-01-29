namespace Customer.Managers;
public class SessionManager {
    private static SessionManager? _int;
    public static SessionManager Int {
        get => _int ?? new SessionManager();
        private set => _int = value;
    }

    private readonly List<int> Sessions = [];
    private SessionManager() {

    }

    public dynamic InitSession(AccountIdentityModel Identity) {
        Sessions.Add(1);
        return "";
    }
}
