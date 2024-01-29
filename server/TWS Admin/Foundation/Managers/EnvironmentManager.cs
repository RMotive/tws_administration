using System.Reflection;

using Foundation.Enumerators.Managers;

namespace Foundation.Managers;
public class EnvironmentManager {
    private static EnvironmentModes? _Mode;

    public static EnvironmentModes Mode {
        get {
            if (_Mode is null) LoadEnvironment();
            return (EnvironmentModes)_Mode!;
        }
    }
    public static bool IsQuality {
        get {
            if (_Mode is null) LoadEnvironment();
            return Mode == EnvironmentModes.quality;
        }
    }

    public static bool IsDevelopment {
        get {
            if (_Mode is null) LoadEnvironment();
            return Mode == EnvironmentModes.development;
        }
    }


    private static void LoadEnvironment() {
        Assembly[] ApplicationAssemblies = AppDomain.CurrentDomain.GetAssemblies();

        bool RunningQuality = ApplicationAssemblies
            .Any(i => (i.FullName?.StartsWith("xunit.runner", StringComparison.InvariantCultureIgnoreCase)) ?? false);

        if (RunningQuality) {
            _Mode = EnvironmentModes.quality;
            return;
        }

        _Mode = EnvironmentModes.development;
    }
}
