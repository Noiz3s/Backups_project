using Backups.Extra.Entities;

namespace Backups.Extra.Models;

public interface ILogger
{
    void CreateLog(BackupTaskExtra task);
}