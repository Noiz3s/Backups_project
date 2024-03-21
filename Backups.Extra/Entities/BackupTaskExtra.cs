using Backups.Entities;
using Backups.Exceptions;
using Backups.Extra.Exceptions;
using Backups.Extra.Models;
using Backups.Models;

namespace Backups.Extra.Entities;

public class BackupTaskExtra : BackupTask
{
    private string _output;
    private List<string> _inspectedFiles;
    private BackupExtra _backup;
    private InMemoryRepositoryExtra _repo;

    public BackupTaskExtra(List<string> inspectedFiles, string output, int id, IRepository repo)
        : base(inspectedFiles, output, id, repo)
    {
        _output = output;
        _inspectedFiles = inspectedFiles;
        _backup = new BackupExtra();
        _repo = repo as InMemoryRepositoryExtra ??
                throw BackupTaskExceptions.NullRepositoryException("Tried to use null repo");
    }

    public BackupExtra BackupExtra => _backup;
    public InMemoryRepositoryExtra Repo => _repo;
    public void SetBackupExtra()
    {
        _backup.SetRestorePoints(Backup.RestorePoints);
    }

    public void RestoreTo(int ind, string path)
    {
        _output = path ?? throw BackupExtraExceptions.NullPathException("tried to restore to null path");
        RestorePoint temp = _backup.RestorePoints.ToList()[ind];
        foreach (Storage storage in temp.Storages)
        {
            foreach (IBackupObject backupObject in storage.Objects)
            {
                foreach (string filepath in backupObject.Objects)
                {
                    for (int i = filepath.Length - 1; i >= 0; i--)
                    {
                        if (filepath[i] != Path.DirectorySeparatorChar) continue;
                        string outputTemp = _output + filepath[i..];
                        File.Copy(filepath, outputTemp);
                        break;
                    }
                }
            }
        }
    }
}