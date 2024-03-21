namespace Backups.Exceptions;

public class StorageExceptions : Exception
{
    private StorageExceptions(string msg)
        : base(msg) { }

    public static StorageExceptions NullObjectsException(string msg)
    {
        return new StorageExceptions(msg);
    }

    public static StorageExceptions NullInspectedFilesException(string msg)
    {
        return new StorageExceptions(msg);
    }

    public static StorageExceptions NullHandlerException(string msg)
    {
        return new StorageExceptions(msg);
    }
}