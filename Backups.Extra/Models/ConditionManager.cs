using System.Text.Json;
using Backups.Extra.Entities;
using Backups.Extra.Exceptions;

namespace Backups.Extra.Models;

public class ConditionManager
{
    private readonly string _path;

    public ConditionManager(string path)
    {
        _path = path;
    }

    public void SaveCondition(BackupTaskExtra task)
    {
        File.WriteAllText(_path, JsonSerializer.Serialize(task));
    }

    public BackupTaskExtra LoadCondition()
    {
        return JsonSerializer.Deserialize<BackupTaskExtra>(File.ReadAllText(_path)) ??
               throw BackupExtraServiceExceptions.NullBackupException("Tried to load null BackupTask");
    }
}