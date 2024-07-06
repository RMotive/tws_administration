using System.Net;

namespace CSMFoundation.Server.Utils;
static public class ServerUtils {
    static public string GetHost() {
        string hn = Dns.GetHostName();
        IPAddress[] @as = Dns.GetHostAddresses(hn);
        string h = @as.ToList().Where(I => I.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault()?.ToString() ?? "";
        return h;
    }
}
