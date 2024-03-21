using Backups.Entities;
using Backups.Exceptions;
using Backups.Models;
namespace Backups.Algoritms;

public class SplitStorage : IAlgo
{
    private IRepository _repo;

    public SplitStorage(IRepository repo)
    {
        _repo = repo ?? throw AlgoritmsExceptions.NullRepoException("Tried to use algoritm on null repo");
    }

    public IRepository Repository => _repo;

    public void Save(BackupTask task)
    {
        RestorePoint restorePoint = task.Backup.RestorePoints.Last();
        foreach (string file in restorePoint.InspectedFiles)
        {
            _repo.CopyElements(task);
        }
    }

    public void Save(BackupTask task, IReadOnlyCollection<InMemoryFolder> files)
    {
        if (task is null)
        {
            throw AlgoritmsExceptions.NullBackupTaskException("Tried to use algoritm with null BackupTask");
        }

        if (files is null)
        {
            throw AlgoritmsExceptions.NullFilesException("Tried to use algoritm on null files");
        }

        _repo.AddBackupTask(task);
        for (int i = 0; i < files.Count; i++)
        {
            _repo.CopyElements(task);
        }
    }
}