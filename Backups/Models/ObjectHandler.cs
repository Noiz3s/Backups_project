using Backups.Entities;
using Backups.Exceptions;
namespace Backups.Models;

public class ObjectHandler
{
    public IReadOnlyCollection<IBackupObject> TransformToIBackupObjects(IReadOnlyCollection<string> paths)
    {
        if (paths is null)
        {
            throw HandlerExceptions.NullPathsException("Tried to use Handler on files with null path");
        }

        var list = new List<IBackupObject>();
        foreach (string path in paths)
        {
            if (Directory.Exists(path))
            {
                IBackupObject folder = new FolderBackupObject(path);
                list.Add(folder);
            }
            else
            {
                IBackupObject file = new FileBackupObject(path);
                list.Add(file);
            }
        }

        return list;
    }
}