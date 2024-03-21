namespace Backups.Exceptions;

public class RestorePointExceptions : Exception
{
    private RestorePointExceptions(string msg)
        : base(msg) { }

    public static RestorePointExceptions NullFilesException(string msg)
    {
        return new RestorePointExceptions(msg);
    }

    public static RestorePointExceptions NullOutputException(string msg)
    {
        return new RestorePointExceptions(msg);
    }
}