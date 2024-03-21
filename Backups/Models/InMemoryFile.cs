using Backups.Exceptions;
namespace Backups.Models;

public class InMemoryFile : IInMemoryFile
{
    private string _name;
    private string _data;

    public InMemoryFile(string name, string data)
    {
        _name = name ?? throw InMemoryFileExceptions.NullNameException(
            "Tried to create File with null name");
        _data = data ?? throw InMemoryFileExceptions.NullDataException(
            "Tried to create File with null data");
    }

    public string Name => _name;
    public string Data => _data;
}