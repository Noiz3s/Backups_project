using Backups.Exceptions;
using Backups.Models;

namespace Backups.Entities;

public class Repository : IRepository
{
    private List<BackupTask> _backupTasks;
    private ObjectHandler _handler;

    public Repository()
    {
        _backupTasks = new List<BackupTask>();
        _handler = new ObjectHandler();
    }

    public List<BackupTask> BackupTasks => _backupTasks;

    public ObjectHandler Handler => _handler;

    public void AddBackupTask(BackupTask task)
    {
        if (task is null)
        {
            throw RepositoryExceptions.NullBackupTaskException("Tried to add null BackupTask");
        }

        _backupTasks.Add(task);
    }

    public void CopyElements(BackupTask task)
    {
        RestorePoint restorePoint = task.Backup.RestorePoints.Last();
        Storage storage = restorePoint.AddStorage(restorePoint.InspectedFiles, Handler);
        string output = restorePoint.Output;
        if (storage is null)
        {
            throw RepositoryExceptions.NullStorageException("Tried to copy files to null Storage");
        }

        if (output is null)
        {
            throw RepositoryExceptions.NullOutputException("Tried to copy files to null output path");
        }

        string currentOutput = output + Path.DirectorySeparatorChar + storage.Name;
        Directory.CreateDirectory(currentOutput);
        foreach (IBackupObject backupObject in storage.Objects)
        {
            if (backupObject.Type == "Folder")
            {
                currentOutput += Path.DirectorySeparatorChar + backupObject.Name;
                Directory.CreateDirectory(currentOutput);
            }

            foreach (string path in backupObject.Objects)
            {
                File.Copy(
                    path, currentOutput + Path.DirectorySeparatorChar + backupObject.Name);
            }
        }
    }
}