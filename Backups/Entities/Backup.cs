using Backups.Exceptions;
namespace Backups.Entities;

public class Backup
{
    private int _restorePointId = 1;
    private List<RestorePoint> _restorePoints;

    public Backup()
    {
        _restorePoints = new List<RestorePoint>();
    }

    public IReadOnlyCollection<RestorePoint> RestorePoints => _restorePoints;
    public int RestorePointId => _restorePointId;
    public RestorePoint AddRestorePoint(DateTime date, IReadOnlyCollection<string> inspectedFiles, string output)
    {
        if (inspectedFiles is null)
        {
            throw BackupExceptions.NullInspectedFilesException("Tried to make RestorePoint with null files");
        }

        if (output is null)
        {
            throw BackupExceptions.NullOutputException("Tried to make RestorePoint with null output path");
        }

        var restorePoint = new RestorePoint(date, inspectedFiles, _restorePointId++, output);

        _restorePoints.Add(restorePoint);
        return restorePoint;
    }
}