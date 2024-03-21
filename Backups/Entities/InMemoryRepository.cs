using Backups.Exceptions;
using Backups.Models;

namespace Backups.Entities;

public class InMemoryRepository : IRepository
{
    private readonly List<BackupTask> _backupTasks;
    private List<int> _taskRestorePointIds;
    private List<int> _taskStorageIds;
    private List<int> _taskfilesIds;
    private InMemoryFolder _folder;
    private List<InMemoryFolder> _files;

    public InMemoryRepository(InMemoryFolder folder, List<InMemoryFolder> files)
    {
        _folder = folder ?? throw RepositoryExceptions.NullFolderException(
                      "Tried to create Repository with null folder");
        _backupTasks = new List<BackupTask>();
        _taskRestorePointIds = new List<int>();
        _taskStorageIds = new List<int>();
        _files = files ?? throw RepositoryExceptions.NullFilesException(
            "Tried to create Repository with null files to copy");
        _taskfilesIds = new List<int>();
    }

    public List<BackupTask> BackupTasks => _backupTasks;
    public InMemoryFolder Folder => _folder;
    public void AddBackupTask(BackupTask task)
    {
        if (task is null)
        {
            throw RepositoryExceptions.NullBackupTaskException("Tried to add null BackupTask to Repository");
        }

        var newFolder = new InMemoryFolder(task.Name, new List<IInMemoryFile>());
        _folder.Data.Add(newFolder);
        _backupTasks.Add(task);
        _taskStorageIds.Add(1);
        _taskRestorePointIds.Add(1);
        _taskfilesIds.Add(0);
    }

    public void CopyElements(BackupTask task)
    {
        if (task is null)
        {
            throw RepositoryExceptions.NullBackupTaskException(
                "Tried to execute null BackupTask in Repository");
        }

        if (_taskfilesIds[_backupTasks.IndexOf(task)] >= _files.Count)
        {
            _taskfilesIds[_backupTasks.IndexOf(task)] = 0;
        }

        InMemoryFolder restorePoint;

        if (_taskfilesIds[_backupTasks.IndexOf(task)] == 0)
        {
            restorePoint =
                AddRestorePoint(task, "RestorePoint " + _taskRestorePointIds[_backupTasks.IndexOf(task)]++);
        }
        else
        {
            restorePoint = (_folder.Data[_backupTasks.IndexOf(task)] as InMemoryFolder) !;
            restorePoint = (restorePoint.Data.Last() as InMemoryFolder) !;
        }

        InMemoryFolder currentFolder =
            AddStorage(restorePoint, "Storage " + _taskStorageIds[_backupTasks.IndexOf(task)]++);
        foreach (IInMemoryFile file in _files[_taskfilesIds[_backupTasks.IndexOf(task)]++].Data)
        {
            AddFile(currentFolder, file);
        }
    }

    private InMemoryFolder AddRestorePoint(BackupTask task, string name)
    {
        if (task is null)
        {
            throw RepositoryExceptions.NullBackupTaskException("Tried to add RestorePoint to null BackupTask");
        }

        if (name is null)
        {
            throw RepositoryExceptions.NullNameException("Tried to add RestorePoint with null name");
        }

        var taskFolder = _folder.Data.FirstOrDefault(file => (file is InMemoryFolder) && (file.Name == task.Name)) as InMemoryFolder;
        if (taskFolder is null)
        {
            throw RepositoryExceptions.NullBackupTaskFolderException(
                "Tried to use folder of unexisting BackupTask");
        }

        var folder = new InMemoryFolder(name, new List<IInMemoryFile>());
        taskFolder.AddFile(folder);
        return folder;
    }

    private InMemoryFolder AddStorage(InMemoryFolder restorePoint, string name)
    {
        if (restorePoint is null)
        {
            throw RepositoryExceptions.NullRestorePointException("Tried to use null RestorePoint");
        }

        if (name is null)
        {
            throw RepositoryExceptions.NullNameException("Tried to add Storage with null name");
        }

        var folder = new InMemoryFolder(name, new List<IInMemoryFile>());
        restorePoint.AddFile(folder);
        return folder;
    }

    private void AddFile(InMemoryFolder storage, IInMemoryFile file)
    {
        if (storage is null)
        {
            throw RepositoryExceptions.NullStorageException("Tried to add file to null Storage");
        }

        if (file is null)
        {
            throw RepositoryExceptions.NullFileException("Tried to add null File to storage");
        }

        storage.AddFile(file);
    }
}