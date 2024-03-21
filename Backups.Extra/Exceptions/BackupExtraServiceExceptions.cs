namespace Backups.Extra.Exceptions;

public class BackupExtraServiceExceptions : Exception
{
    private BackupExtraServiceExceptions(string msg)
        : base(msg) { }

    public static BackupExtraServiceExceptions NullBackupException(string msg)
    {
        return new BackupExtraServiceExceptions(msg);
    }

    public static BackupExtraServiceExceptions NullAlgoException(string msg)
    {
        return new BackupExtraServiceExceptions(msg);
    }

    public static BackupExtraServiceExceptions NullFilesException(string msg)
    {
        return new BackupExtraServiceExceptions(msg);
    }
}