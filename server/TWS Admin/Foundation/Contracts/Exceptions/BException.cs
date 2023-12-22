namespace Foundation.Contracts.Exceptions;
public abstract class BException
    : Exception
{
    public BException(string Message)
        : base(Message)
    { }

    public abstract Dictionary<string, dynamic> ToDisplay();
}
