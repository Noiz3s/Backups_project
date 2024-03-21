using Backups.Entities;
using Backups.Extra.Exceptions;
using Backups.Models;
namespace Backups.Extra.Entities;

public class BackupExtra : Backup
{
    private List<RestorePoint> _restorePoints;
    private string _log;

    public BackupExtra()
    {
        _restorePoints = new List<RestorePoint>();
        _log = string.Empty;
    }

    public string Log => _log;
    public List<RestorePoint> Points => _restorePoints;
    public void SetRestorePoints(IReadOnlyCollection<RestorePoint> restorePoints)
    {
        _restorePoints = restorePoints.ToList();
        ChangeLog(_restorePoints.Last().Name + " created");
    }

    public void ChangeLog(string log)
    {
        _log = log ?? throw BackupExtraExceptions.NullLogException("Tried to add null log");
    }

    public void ClearFolder(string path)
    {
        var directoryInfo = new DirectoryInfo(path);
        foreach (FileInfo fileInfo in directoryInfo.GetFiles())
        {
            fileInfo.Delete();
        }

        foreach (DirectoryInfo tempDirectoryInfo in directoryInfo.GetDirectories())
        {
            ClearFolder(tempDirectoryInfo.FullName);
            tempDirectoryInfo.Delete();
        }
    }
}