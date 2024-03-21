using Backups.Algoritms;
using Backups.Exceptions;
using Backups.Models;

namespace Backups.Entities;

public class BackupTask
{
    private readonly string _output;
    private readonly string _name;
    private List<string> _inspectedFiles;
    private Backup _backup;
    private IRepository _repo;

    public BackupTask(List<string> inspectedFiles, string output, int id, IRepository repo)
    {
        _name = "BackupTask " + id;
        _backup = new Backup();
        _output = output ?? throw BackupTaskExceptions.NullPathException(
            "Tried to create BackupTask connected to repo with null path");
        _inspectedFiles = inspectedFiles ?? throw BackupTaskExceptions.NullInspectedFilesException(
            "Tried to Inspect null files");
        _repo = repo ?? throw BackupTaskExceptions.NullRepositoryException("Tried to use null Repository");
    }

    public Backup Backup => _backup;
    public string Output => _output;
    public string Name => _name;
    public IReadOnlyCollection<string> InspectedFiles => _inspectedFiles;

    public void SetInspectedFiles(List<string> newFiles)
    {
        if (newFiles is null)
        {
            throw BackupTaskExceptions.NullInspectedFilesException(
                "Tried to use BackupTask with null files");
        }

        _inspectedFiles = newFiles;
    }

    public void ExecuteTask(IAlgo algo)
    {
        if (algo is null)
        {
            throw BackupTaskExceptions.NullAlgoException("Tried to use null algoritm on BackupTask");
        }

        _backup.AddRestorePoint(DateTime.Now, _inspectedFiles, _output + Path.DirectorySeparatorChar + _name);
        algo.Save(this);
    }

    public void ExecuteTask(IAlgo algo, IReadOnlyCollection<InMemoryFolder> files)
    {
        if (algo is null)
        {
            throw BackupTaskExceptions.NullAlgoException("Tried to use null algoritm on BackupTask");
        }

        algo.Save(this, files);
    }
}