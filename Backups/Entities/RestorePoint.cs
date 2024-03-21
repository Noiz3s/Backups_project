using Backups.Exceptions;
using Backups.Models;
namespace Backups.Entities;

public class RestorePoint
{
    private readonly DateTime _date;
    private readonly string _name;
    private readonly string _output;
    private int _storageId = 1;
    private List<Storage> _storages;
    private IReadOnlyCollection<string> _inspectedFiles;
    public RestorePoint(DateTime date, IReadOnlyCollection<string> inspectedFiles, int id, string output)
    {
        if (output is null)
        {
            throw RestorePointExceptions.NullOutputException(
                "Tried to create RestorePoint with null output path");
        }

        _name = "RestorePoint " + id;
        _date = date;
        _storages = new List<Storage>();
        _inspectedFiles = inspectedFiles ??
                          throw RestorePointExceptions.NullFilesException(
                              "Tried to create RestorePoint with null files");
        _output = output + System.IO.Path.DirectorySeparatorChar + _name;
    }

    public string Name => _name;
    public DateTime Date => _date;
    public IReadOnlyCollection<Storage> Storages => _storages;
    public IReadOnlyCollection<string> InspectedFiles => _inspectedFiles;
    public string Output => _output;

    public Storage AddStorage(IReadOnlyCollection<string> inspectedFiles, ObjectHandler handler)
    {
        var storage = new Storage(_storageId++, inspectedFiles, handler);
        _storages.Add(storage);
        return storage;
    }
}