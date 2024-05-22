using Customer.Managers.Records;
using Customer.Services.Records;

using TWS_Security.Sets;

namespace Customer.Managers;
public sealed class SessionsManager {
    private static SessionsManager? Instance;
    static public SessionsManager Manager {
        get {
            return Instance ??= new SessionsManager();
        }
    }


    readonly TimeSpan EXPIRATION_RANGE = TimeSpan.FromHours(2);
    readonly List<Session> Sessions = [];
    private SessionsManager() {

    }

    private static bool EvaluateAlive(DateTime Expiration) {
        return DateTime.Compare(DateTime.Now, Expiration) >= 0;
    }
    private Guid Tokenize() {
        Guid token = Guid.NewGuid();
        Session? session = Sessions
            .Where(i => i.Token.ToString() == token.ToString())
            .FirstOrDefault();
        if (session == null)
            return token;
        return Tokenize();
    }
    private Session? Clean(string Identity) {
        Session? session = Sessions
            .Where(i => i.Identity == Identity)
            .FirstOrDefault();
        if (session is null)
            return null;

        if (DateTime.Compare(DateTime.UtcNow, session.Expiration) < 0)
            return session;

        Sessions.Remove(session);
        return null;
    }
    private Session? TClean(string Token) {
        Session? session = Sessions
            .Where(i => i.Token.ToString() == Token)
            .FirstOrDefault();
        if (session is null)
            return null;

        if (DateTime.Compare(DateTime.UtcNow, session.Expiration) < 0)
            return session;

        Sessions.Remove(session);
        return null;
    }
    private Session Refresh(Session session) {
        int position = Sessions.IndexOf(session);

        Session refreshed = new() {
            Expiration = DateTime.UtcNow.Add(EXPIRATION_RANGE),
            Wildcard = session.Wildcard,
            Identity = session.Identity,
            Token = session.Token,
            Permits = session.Permits,
        };
        Sessions[position] = refreshed;
        return refreshed;
    }
    public Session Subscribe(Credentials Credentials, bool Wildcard, Permit[] Permits) {
        Session? session = Clean(Credentials.Identity);

        if (session is not null)
            return Refresh(session);

        session = new() {
            Expiration = DateTime.Now.Add(EXPIRATION_RANGE),
            Identity = Credentials.Identity,
            Wildcard = Wildcard,
            Token = Tokenize(),
            Permits = Permits,
        };
        Sessions.Add(session);
        return session;
    }
    public bool EvaluateExpiration(string Token) {
        Session? session = TClean(Token);
        if (session is null)
            return false;

        return EvaluateAlive(session.Expiration);
    }
    public bool EvaluateWildcard(string Token) {
        Session? session = TClean(Token);
        if (session is null)
            return false;

        return session.Wildcard;
    }
}
