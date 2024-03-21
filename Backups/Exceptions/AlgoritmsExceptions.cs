namespace Backups.Exceptions;

public class AlgoritmsExceptions : Exception
{
    private AlgoritmsExceptions(string msg)
        : base(msg) { }

    public static AlgoritmsExceptions NullRepoException(string msg)
    {
        return new AlgoritmsExceptions(msg);
    }

    public static AlgoritmsExceptions NullFilesException(string msg)
    {
        return new AlgoritmsExceptions(msg);
    }

    public static AlgoritmsExceptions NullBackupTaskException(string msg)
    {
        return new AlgoritmsExceptions(msg);
    }
}