using System.Reflection;

using CSM_Foundation.Server.Enumerators;

namespace CSM_Foundation.Server.Managers;
public class EnvironmentManager {
    private static ServerEnvironments? _Mode;

    public static ServerEnvironments Mode {
        get {
            if (_Mode is null) {
                LoadEnvironment();
            }

            return (ServerEnvironments)_Mode!;
        }
    }
    public static bool IsQuality {
        get {
            if (_Mode is null) {
                LoadEnvironment();
            }

            return Mode == ServerEnvironments.quality;
        }
    }

    public static bool IsDevelopment {
        get {
            if (_Mode is null) {
                LoadEnvironment();
            }

            return Mode == ServerEnvironments.development;
        }
    }


    private static void LoadEnvironment() {
        Assembly[] ApplicationAssemblies = AppDomain.CurrentDomain.GetAssemblies();

        bool RunningQuality = ApplicationAssemblies
            .Any(i => (i.FullName?.StartsWith("xunit.runner", StringComparison.InvariantCultureIgnoreCase)) ?? false);

        if (RunningQuality) {
            _Mode = ServerEnvironments.quality;
            return;
        }

        _Mode = ServerEnvironments.development;
    }
}
