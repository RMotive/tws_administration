using Foundation.Contracts.Datasources.Interfaces;
using Foundation.Contracts.Exceptions.Bases;
using Foundation.Enumerators.Exceptions;

namespace Foundation.Exceptions.Datasources;

/// <summary>
///     This class represent a datasource exception when a Set record is not found.
///     
///     Used when a record is request from a datasource repository, and it isn't found.
/// </summary>
public class XRecordUnfound<TRepository>
    : BException
    where TRepository : IRepository {
    new const string Message = "Unable to found the requested record";

    public Type Repository { get; private set; }
    public string Method { get; private set; }
    public dynamic Reference { get; private set; }
    public RecordSearchMode Mode { get; private set; }

    public XRecordUnfound(string Method, dynamic Reference, RecordSearchMode Mode)
        : base($"{Message} on ({typeof(TRepository)}) : ({Method}) | with ({Reference})") {
        Repository = typeof(TRepository);
        this.Method = Method;
        this.Reference = Reference;
        this.Mode = Mode;
    }
}
