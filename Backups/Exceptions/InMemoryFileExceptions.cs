using Backups.Models;

namespace Backups.Exceptions;

public class InMemoryFileExceptions : Exception
{
    private InMemoryFileExceptions(string msg)
        : base(msg) { }

    public static InMemoryFileExceptions NullNameException(string msg)
    {
        return new InMemoryFileExceptions(msg);
    }

    public static InMemoryFileExceptions NullDataException(string msg)
    {
        return new InMemoryFileExceptions(msg);
    }

    public static InMemoryFileExceptions NullFileException(string msg)
    {
        return new InMemoryFileExceptions(msg);
    }
}