﻿using System.Runtime.CompilerServices;
using System.Text.Json;

using Foundation.Migration.Records;
using Foundation.Server.Enumerators;
using Foundation.Server.Managers;

namespace Foundation.Migration.Utils;
public class MigrationUtils {
    private const string DirectoryName = "Connection";
    private const string QualityPrefix = "quality_";
    private const string DevelopmentPrefix = "development_";

    /// <summary>
    ///     Fetches and loads through IO functionallities for private file based secret
    ///     connection properties for Datasources connections handlers. 
    ///     Then build it and validate it to generate a Model.
    ///     
    ///     RECOMMENDED: Check the parameter documentation to ensure the correct use.
    /// </summary>
    /// <param name="cp"> 
    ///     Automatically gets the path of the method caller through execution-time assemblies access
    ///     attribute decorator, this is correctly calculated when the method is called
    ///     directly in the Datasource context class that is generated by the EF CLI tools when 
    ///     migrated a database configuration, otherwise can struggle with the correct project 
    ///     file leveling generation. 
    ///     In that kind of cases this parameter can be overwritten by the caller to inject
    ///     the correct datasource project root path.
    /// </param>
    /// <returns>
    ///     <see cref="DatasourceConnectionModel"/>: The datasource connection properties gathered and retrieved from the found private properties file.
    /// </returns>
    /// <exception cref="XDatasourceConnectionLoad">
    ///     When something gone wrong during the IO connection properties gather operation.
    /// </exception>
    public static MigrationConnectionOptions Retrieve([CallerFilePath] string? cp = null) {
        string prefix = EnvironmentManager.Mode switch {
            ServerEnvironments.development => DevelopmentPrefix,
            ServerEnvironments.quality => QualityPrefix,
            _ => DevelopmentPrefix,
        };
        string fn = $"{prefix}connection.json";

        if (cp is null)
            throw new ArgumentNullException(cp);
        DirectoryInfo? parent = Directory.GetParent(cp);
        if (parent is null)
            throw new DirectoryNotFoundException();
        IEnumerable<DirectoryInfo> pds = parent.EnumerateDirectories();
        DirectoryInfo? cpd = pds
            .Where(i => i.Name == DirectoryName)
            .FirstOrDefault();
        if (cpd is null)
            throw new DirectoryNotFoundException($"{parent.FullName}\\{DirectoryName} not found in the system");
        FileInfo? cpfi = cpd.GetFiles()
            .Where(i => i.Name == fn)
            .FirstOrDefault();
        if (cpfi is null)
            throw new FileNotFoundException();

        using FileStream pfs = new(cpfi.FullName, FileMode.Open, FileAccess.Read, FileShare.Read);
        MigrationConnectionOptions? m = JsonSerializer.Deserialize<MigrationConnectionOptions>(pfs);
        pfs.Dispose();

        if (m is null)
            throw new Exception();
        return m;
    }
}
