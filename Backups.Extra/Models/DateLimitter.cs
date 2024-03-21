using Backups.Entities;

namespace Backups.Extra.Models;

public class DateLimitter : ILimitter
{
    private int _maxDateDiff;

    public DateLimitter(int maxDateDiff)
    {
        _maxDateDiff = maxDateDiff;
    }

    public int GetOutdatedAmount(List<RestorePoint> restorePoints)
    {
        RestorePoint cur = restorePoints.Last();

        return restorePoints.Count(point => (cur.Date - point.Date).TotalSeconds > _maxDateDiff);
    }
}