using Foundation.Contracts.Exceptions;

namespace Foundation.Contracts.Records;
public interface IFailure {
    public BException Failure {  get; set; } }
