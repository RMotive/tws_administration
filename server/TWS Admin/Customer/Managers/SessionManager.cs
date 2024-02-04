using Customer.Models;

namespace Customer.Managers;
public class SessionManager {
    private static SessionManager? _int;

    public static SessionManager Int {
        get => _int ?? new SessionManager();
        private set => _int = value;
    }

    private readonly List<SessionModel> Sessions = [];
    private SessionManager() {

    }
    public SessionModel InitSession(AccountIdentityModel accountIdentity) {
        SessionModel? SessionAssigned = Sessions
            .Where(I => I.Identity == accountIdentity)
            .FirstOrDefault();
        
        Guid Token = GenerateToken();
        SessionModel Session = new(Token, false, accountIdentity, new TimeSpan(0, 10, 0), []);
        if(SessionAssigned != null)
            Sessions.Remove(SessionAssigned);
        Sessions.Add(Session);
        return Session;
    }

    /// <summary>
    ///     Recursively generates a token and compare it with the current sessions tokens and if it is in use will try again.
    /// </summary>
    /// <returns> 
    ///    <seealso cref="Guid"/>: Valid identification unique token.
    /// </returns>
    private Guid GenerateToken() {
        Guid Token = Guid.NewGuid();
        foreach(SessionModel Session in Sessions) {
            if(Session.Token != Token) continue; 

            GenerateToken();
            break;
        }
        return Token;
    }
}
