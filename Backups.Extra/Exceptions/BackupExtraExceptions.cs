namespace Backups.Extra.Exceptions;
public class BackupExtraExceptions : Exception
{
    private BackupExtraExceptions(string msg)
        : base(msg) { }

    public static BackupExtraExceptions WrongRestorePointsSizeException(string msg)
    {
        return new BackupExtraExceptions(msg);
    }

    public static BackupExtraExceptions NullLogException(string msg)
    {
        return new BackupExtraExceptions(msg);
    }

    public static BackupExtraExceptions NullPathException(string msg)
    {
        return new BackupExtraExceptions(msg);
    }

    public static BackupExtraExceptions WrongBackupException(string msg)
    {
        return new BackupExtraExceptions(msg);
    }
}