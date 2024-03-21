namespace Backups.Exceptions;

public class BackupTaskExceptions : Exception
{
    private BackupTaskExceptions(string msg)
        : base(msg) { }

    public static BackupTaskExceptions NullPathException(string msg)
    {
        return new BackupTaskExceptions(msg);
    }

    public static BackupTaskExceptions NullInspectedFilesException(string msg)
    {
        return new BackupTaskExceptions(msg);
    }

    public static BackupTaskExceptions NullAlgoException(string msg)
    {
        return new BackupTaskExceptions(msg);
    }

    public static BackupTaskExceptions NullRepositoryException(string msg)
    {
        return new BackupTaskExceptions(msg);
    }
}