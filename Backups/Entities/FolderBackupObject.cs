using Backups.Exceptions;
namespace Backups.Entities;

public class FolderBackupObject : IBackupObject
{
    private readonly string _path;
    private readonly string _name;
    private IReadOnlyCollection<string> _objects;

    public FolderBackupObject(string path)
    {
        _name = string.Empty;
        _path = path ?? throw BackupObjectExceptions.NullPathException(
            "Tried to create BackupObject with null path");
        IEnumerable<string> tempObjects = Directory.EnumerateFiles(
            path, ".", SearchOption.AllDirectories);
        _objects = tempObjects.Select(obj => obj).ToList();
        for (int i = _path.Length - 1; i >= 0; i--)
        {
            if (_path[i] != System.IO.Path.DirectorySeparatorChar) continue;
            _name = _path[i..];
            break;
        }
    }

    public string Type => "Folder";
    public string Path => _path;
    public string Name => _name;
    public IReadOnlyCollection<string> Objects => _objects;
}