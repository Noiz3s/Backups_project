namespace Backups.Exceptions;

public class BackupObjectExceptions : Exception
{
    private BackupObjectExceptions(string msg)
        : base(msg) { }

    public static BackupObjectExceptions NullPathException(string msg)
    {
        return new BackupObjectExceptions(msg);
    }
}