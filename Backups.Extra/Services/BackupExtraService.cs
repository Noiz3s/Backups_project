using Backups.Algoritms;
using Backups.Extra.Entities;
using Backups.Extra.Exceptions;
using Backups.Extra.Models;
using Backups.Models;
namespace Backups.Extra.Services;

public class BackupExtraService
{
    private readonly bool _isMerging;
    private List<BackupExtra> _backups;
    private ILimitter _limitter;
    private ILogger _logger;
    private ConditionManager _manager;
    private int _count;
    private IRestorePointRemover _remover;

    public BackupExtraService(ILimitter limitter, bool isMerging, ILogger logger, ConditionManager manager)
    {
        _backups = new List<BackupExtra>();
        _limitter = limitter;
        _isMerging = isMerging;
        _logger = logger;
        _manager = manager;
        _count = -1;
        _remover = new RestorePointMerger();
    }

    public ILimitter Limitter => _limitter;
    public List<BackupExtra> BackupsList => _backups;
    public ILogger Logger => _logger;
    public int Count => _count;

    public void ExecuteBackUpTask(BackupTaskExtra task, IAlgo algo, IReadOnlyCollection<InMemoryFolder> files)
    {
        AddBackup(task.BackupExtra);
        if (algo is null)
        {
            throw BackupExtraServiceExceptions.NullAlgoException("Tried to use null Algoritm");
        }

        if (files is null)
        {
            throw BackupExtraServiceExceptions.NullFilesException("Tried to use null list of files");
        }

        task.ExecuteTask(algo, files);
        task.Repo.BindRestorePoint(task);
        _logger.CreateLog(task);
        int temp = _limitter.GetOutdatedAmount(task.BackupExtra.Points.ToList());
        _count = task.BackupExtra.Points.Count;
        for (int i = 0; i < temp; i++)
        {
            if (_isMerging)
            {
                _remover = new RestorePointMerger();
                _remover.ExecuteInMemory(task);
                _logger.CreateLog(task);
            }
            else
            {
                _remover = new RestorePointRemover();
                _remover.ExecuteInMemory(task);
                _logger.CreateLog(task);
            }
        }
    }

    public void ExecuteBackUpTask(BackupTaskExtra task, IAlgo algo)
    {
        AddBackup(task.BackupExtra);
        if (algo is null)
        {
            throw BackupExtraServiceExceptions.NullAlgoException("Tried to use null Algoritm");
        }

        task.ExecuteTask(algo);
        task.SetBackupExtra();
        _logger.CreateLog(task);
        int temp = _limitter.GetOutdatedAmount(task.BackupExtra.RestorePoints.ToList());
        for (int i = 0; i < temp; i++)
        {
            if (_isMerging)
            {
                _remover = new RestorePointMerger();
                _remover.ExecuteInFiles(task);
                _logger.CreateLog(task);
            }
            else
            {
                _remover = new RestorePointRemover();
                _remover.ExecuteInFiles(task);
                _logger.CreateLog(task);
            }
        }
    }

    public void RestoreBackup(BackupTaskExtra task, int ind, string path)
    {
        if (_backups.Contains(task.BackupExtra) == false)
        {
            throw BackupExtraExceptions.WrongBackupException("Tried to restore unexisting Backup");
        }

        task.RestoreTo(ind, path);
    }

    public void SaveCondition(BackupTaskExtra task)
    {
        _manager.SaveCondition(task);
    }

    public BackupTaskExtra LoadCondition()
    {
       return _manager.LoadCondition();
    }

    private void AddBackup(BackupExtra backup)
    {
        if (backup is null)
        {
            throw BackupExtraServiceExceptions.NullBackupException("Tried to add null Backup");
        }

        _backups.Add(backup);
    }
}