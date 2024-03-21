using Backups.Exceptions;
using Backups.Extra.Entities;
using Backups.Extra.Exceptions;
using Backups.Models;
namespace Backups.Extra.Models;

public class RestorePointRemover : IRestorePointRemover
{
    public RestorePointRemover()
    { }
    public void ExecuteInMemory(BackupTaskExtra task)
    {
        InMemoryRepositoryExtra repo = task.Repo;
        var taskFolder = repo.Folder.Data.FirstOrDefault(file => (file is InMemoryFolder) && (file.Name == task.Name)) as InMemoryFolder;
        if (taskFolder is null)
        {
            throw RepositoryExceptions.NullBackupTaskFolderException(
                "Tried to use folder of unexisting BackupTask");
        }

        string tempName = taskFolder.Data[0].Name;
        taskFolder.RemoveFile(taskFolder.Data[0]);
        task.BackupExtra.ChangeLog(tempName + " deleted");
    }

    public void ExecuteInFiles(BackupTaskExtra task)
    {
        BackupExtra backup = task.BackupExtra;
        if (backup.RestorePoints.Count == 0)
        {
            throw BackupExtraExceptions.WrongRestorePointsSizeException("Tried to remove RestorePoint from empty list");
        }

        backup.ChangeLog(backup.Points[0].Name + " deleted");
        backup.ClearFolder(backup.Points[0].Name);
        Directory.Delete(backup.Points[0].Name);
        var list = backup.RestorePoints.ToList();
        list.RemoveAt(0);
        backup.SetRestorePoints(list);
    }
}