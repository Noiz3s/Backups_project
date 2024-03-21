using Backups.Entities;
using Backups.Exceptions;
using Backups.Extra.Entities;
using Backups.Extra.Exceptions;
using Backups.Models;

namespace Backups.Extra.Models;

public class RestorePointMerger : IRestorePointRemover
{
    private RestorePointRemover _remover;

    public RestorePointMerger()
    {
        _remover = new RestorePointRemover();
    }

    public void ExecuteInFiles(BackupTaskExtra task)
    {
        BackupExtra backup = task.BackupExtra;
        if (backup.RestorePoints.Count == 0)
        {
            throw BackupExtraExceptions.WrongRestorePointsSizeException("Tried to remove RestorePoint from empty list");
        }

        RestorePoint oldest = backup.Points[0];
        string oldestName = oldest.Name;
        if (oldest.Storages.Count == 1)
        {
            _remover.ExecuteInFiles(task);
        }

        RestorePoint newest = backup.Points[^1];
        string newestName = newest.Name;
        foreach (Storage storage in oldest.Storages)
        {
            var tempList = new List<string>();
            foreach (IBackupObject obj in storage.Objects)
            {
                tempList.AddRange(obj.Objects);
            }

            if (!newest.Storages.Contains(storage))
            {
                newest.AddStorage(tempList, new ObjectHandler());
            }
        }

        _remover.ExecuteInFiles(task);
        backup.ChangeLog(oldestName + " merged to " + newestName);
    }

    public void ExecuteInMemory(BackupTaskExtra task)
    {
        var taskFolder = task.Repo.Folder.Data.FirstOrDefault(file => (file is InMemoryFolder) && (file.Name == task.Name)) as InMemoryFolder;
        if (taskFolder is null)
        {
            throw RepositoryExceptions.NullBackupTaskFolderException(
                "Tried to use folder of unexisting BackupTask");
        }

        var oldest = (taskFolder.Data[0] as InMemoryFolder) !;
        var newest = (taskFolder.Data[^1] as InMemoryFolder) !;
        string oldestName = oldest.Name;
        string newestName = newest.Name;
        if (oldest.Data.Count == 1)
        {
            _remover.ExecuteInMemory(task);
            task.BackupExtra.ChangeLog(oldestName + " deleted");
            return;
        }

        foreach (IInMemoryFile file in oldest.Data.Where(file => newest.Data.Contains(file) == false))
        {
            newest.Data.Add(file);
        }

        _remover.ExecuteInMemory(task);
        task.BackupExtra.ChangeLog(oldestName + " merged to " + newestName);
    }
}