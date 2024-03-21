using Backups.Entities;
using Backups.Exceptions;
using Backups.Models;

namespace Backups.Algoritms;

public class SingleStorage : IAlgo
{
    private IRepository _repo;

    public SingleStorage(IRepository repo)
    {
        _repo = repo ?? throw AlgoritmsExceptions.NullRepoException("Tried to use algoritm on null repo");
    }

    public IRepository Repo => _repo;

    public void Save(BackupTask task)
    {
        _repo.CopyElements(task);
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
        _repo.CopyElements(task);
    }
}