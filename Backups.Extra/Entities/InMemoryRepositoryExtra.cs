using Backups.Entities;
using Backups.Exceptions;
using Backups.Models;
namespace Backups.Extra.Entities;

public class InMemoryRepositoryExtra : InMemoryRepository
{
    private readonly List<BackupTask> _backupTasks;
    private List<int> _taskRestorePointIds;
    private List<int> _taskStorageIds;
    private List<int> _taskfilesIds;
    private InMemoryFolder _folder;
    private List<InMemoryFolder> _files;
    private int _id;

    public InMemoryRepositoryExtra(InMemoryFolder folder, List<InMemoryFolder> files)
        : base(folder, files)
    {
        _folder = folder ?? throw RepositoryExceptions.NullFolderException(
            "Tried to create Repository with null folder");
        _backupTasks = new List<BackupTask>();
        _taskRestorePointIds = new List<int>();
        _taskStorageIds = new List<int>();
        _files = files ?? throw RepositoryExceptions.NullFilesException(
            "Tried to create Repository with null files to copy");
        _taskfilesIds = new List<int>();
        _id = 1;
    }

    public void BindRestorePoint(BackupTaskExtra task)
    {
        var temp = task.BackupExtra.Points.ToList();
        temp.Add(new RestorePoint(DateTime.Now, new List<string>(), _id++, string.Empty));
        task.BackupExtra.SetRestorePoints(temp);
    }
}