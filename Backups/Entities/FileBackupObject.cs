using Backups.Exceptions;
namespace Backups.Entities;

public class FileBackupObject : IBackupObject
{
    private string _path;
    private string _name;
    public FileBackupObject(string path)
    {
        _name = string.Empty;
        _path = path ?? throw BackupObjectExceptions.NullPathException(
            "Tried to create BackupObject with null path");
        for (int i = _path.Length - 1; i >= 0; i--)
        {
            if (_path[i] != System.IO.Path.DirectorySeparatorChar) continue;
            _name = _path[i..];
            break;
        }
    }

    public string Name => _name;
    public string Type => "File";
    public string Path => _path;
    public IReadOnlyCollection<string> Objects => new List<string> { _path };
}