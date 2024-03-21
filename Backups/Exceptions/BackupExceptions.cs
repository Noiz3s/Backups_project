using Backups.Entities;

namespace Backups.Exceptions;

public class BackupExceptions : Exception
{
    private BackupExceptions(string msg)
        : base(msg) { }

    public static BackupExceptions NullInspectedFilesException(string msg)
    {
        return new BackupExceptions(msg);
    }

    public static BackupExceptions NullOutputException(string msg)
    {
        return new BackupExceptions(msg);
    }
}