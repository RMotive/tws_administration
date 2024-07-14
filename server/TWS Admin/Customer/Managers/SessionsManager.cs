using TWS_Customer.Managers.Records;
using TWS_Customer.Services.Records;

using TWS_Security.Sets;

namespace TWS_Customer.Managers;
public sealed class SessionsManager {
    private static SessionsManager? Instance;
    public static SessionsManager Manager => Instance ??= new SessionsManager();

    private readonly TimeSpan EXPIRATION_RANGE = TimeSpan.FromHours(2);
    private readonly List<Session> Sessions = [];
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
        return session == null ? token : Tokenize();
    }
    private Session? Clean(string Identity) {
        Session? session = Sessions
            .Where(i => i.Identity == Identity)
            .FirstOrDefault();
        if (session is null) {
            return null;
        }

        if (DateTime.Compare(DateTime.UtcNow, session.Expiration) < 0) {
            return session;
        }

        _ = Sessions.Remove(session);
        return null;
    }
    private Session? TClean(string Token) {
        Session? session = Sessions
            .Where(i => i.Token.ToString() == Token)
            .FirstOrDefault();
        if (session is null) {
            return null;
        }

        if (DateTime.Compare(DateTime.UtcNow, session.Expiration) < 0) {
            return session;
        }

        _ = Sessions.Remove(session);
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
            Contact = session.Contact
        };
        Sessions[position] = refreshed;
        return refreshed;
    }
    public Session Subscribe(Credentials Credentials, bool Wildcard, Permit[] Permits, Contact Contact) {
        Session? session = Clean(Credentials.Identity);

        if (session is not null) {
            return Refresh(session);
        }

        session = new() {
            Expiration = DateTime.Now.Add(EXPIRATION_RANGE),
            Identity = Credentials.Identity,
            Wildcard = Wildcard,
            Token = Tokenize(),
            Permits = Permits,
            Contact = Contact
        };
        Sessions.Add(session);
        return session;
    }
    public bool EvaluateExpiration(string Token) {
        Session? session = TClean(Token);
        return session is not null && EvaluateAlive(session.Expiration);
    }
    public bool EvaluateWildcard(string Token) {
        Session? session = TClean(Token);
        return session is not null && session.Wildcard;
    }
}
