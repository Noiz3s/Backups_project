using Backups.Entities;
using Backups.Exceptions;
namespace Backups.Models;

public class Storage : IStorage
{
    private readonly IReadOnlyCollection<IBackupObject> _objects;
    private readonly string _name;

    public Storage(int id, IReadOnlyCollection<string> inspectedFiles, ObjectHandler handler)
    {
        if (inspectedFiles is null)
        {
            throw StorageExceptions.NullInspectedFilesException("Tried to create storage with null Files");
        }

        if (handler is null)
        {
            throw StorageExceptions.NullHandlerException("Tried to use null Handler on storage");
        }

        _name = "Storage " + id;
        _objects = handler.TransformToIBackupObjects(inspectedFiles);
    }

    public string Name => _name;
    public IReadOnlyCollection<IBackupObject> Objects => _objects;
}