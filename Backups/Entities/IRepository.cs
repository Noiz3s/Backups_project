namespace Backups.Entities;

public interface IRepository
{
    List<BackupTask> BackupTasks { get; }
    void CopyElements(BackupTask task);
    void AddBackupTask(BackupTask task);
}