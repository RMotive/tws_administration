namespace Foundation.Contracts.Exceptions.Bases;
public abstract class BException
    : Exception {
    public BException(string Message)
        : base(Message) { }

}
