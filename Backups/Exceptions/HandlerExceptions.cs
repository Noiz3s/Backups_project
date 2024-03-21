namespace Backups.Exceptions;

public class HandlerExceptions : Exception
{
    private HandlerExceptions(string msg)
        : base(msg) { }

    public static HandlerExceptions NullPathsException(string msg)
    {
        return new HandlerExceptions(msg);
    }
}