using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TWS_Security.Exceptions.Entities;
public class XBuildDatasourceSet : Exception
{
    private const string defaultMessage = "Error building a datasource set from entity";

    /// <summary>
    ///     Entity that fired up this exception.
    /// </summary>
    public Type Entity;

    public XBuildDatasourceSet(Type EntityType)
        : base($"{defaultMessage} [{EntityType}]")
    {
        Entity = EntityType;
    }
}
