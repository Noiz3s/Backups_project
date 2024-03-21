using Backups.Exceptions;

namespace Backups.Models;

public class InMemoryStorage : IStorage
{
    private readonly string _name;
    private readonly int _id;
    private IReadOnlyCollection<IInMemoryFile> _objects;

    public InMemoryStorage(int id, IReadOnlyCollection<IInMemoryFile> objects)
    {
        _name = "Storage " + id;
        _id = id;
        _objects = objects ?? throw StorageExceptions.NullObjectsException(
            "Tried to create storage with null objects");
    }

    public string Name => _name;
    public int Id => _id;
    public IReadOnlyCollection<IInMemoryFile> Objects => _objects;
}