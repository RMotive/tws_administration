using System.Net;

namespace CSM_Foundation.Server.Utils;
public static class ServerUtils {
    public static string GetHost() {
        string hn = Dns.GetHostName();
        IPAddress[] @as = Dns.GetHostAddresses(hn);
        string h = @as.ToList().Where(I => I.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault()?.ToString() ?? "";
        return h;
    }
}
