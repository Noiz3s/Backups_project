using Backups.Entities;

namespace Backups.Exceptions;

public class RepositoryExceptions : Exception
{
    private RepositoryExceptions(string msg)
        : base(msg) { }

    public static RepositoryExceptions NullBackupTaskException(string msg)
    {
        return new RepositoryExceptions(msg);
    }

    public static RepositoryExceptions NullBackupTaskFolderException(string msg)
    {
        return new RepositoryExceptions(msg);
    }

    public static RepositoryExceptions NullFilesException(string msg)
    {
        return new RepositoryExceptions(msg);
    }

    public static RepositoryExceptions NullFolderException(string msg)
    {
        return new RepositoryExceptions(msg);
    }

    public static RepositoryExceptions NullNameException(string msg)
    {
        return new RepositoryExceptions(msg);
    }

    public static RepositoryExceptions NullRestorePointException(string msg)
    {
        return new RepositoryExceptions(msg);
    }

    public static RepositoryExceptions NullFileException(string msg)
    {
        return new RepositoryExceptions(msg);
    }

    public static RepositoryExceptions NullStorageException(string msg)
    {
        return new RepositoryExceptions(msg);
    }

    public static RepositoryExceptions NullOutputException(string msg)
    {
        return new RepositoryExceptions(msg);
    }
}