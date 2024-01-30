using Foundation.Contracts.Datasources.Interfaces;
using Foundation.Contracts.Exceptions;
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

    private Type Repository { get; set; }
    private string Method { get; set; }
    private dynamic Reference { get; set; }
    private RecordSearchMode Mode { get; set; }

    public XRecordUnfound(string Method, dynamic Reference, RecordSearchMode Mode)
        : base($"{Message} on ({typeof(TRepository)}) : ({Method}) | with ({Reference})") {
        Repository = typeof(TRepository);
        this.Method = Method;
        this.Reference = Reference;
        this.Mode = Mode;
    }
}
