using Backups.Extra.Entities;

namespace Backups.Extra.Models;

public interface IRestorePointRemover
{
    public void ExecuteInMemory(BackupTaskExtra task);
    public void ExecuteInFiles(BackupTaskExtra task);
}