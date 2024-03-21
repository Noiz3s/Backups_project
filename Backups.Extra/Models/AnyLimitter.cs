﻿using Backups.Entities;
namespace Backups.Extra.Models;

public class AnyLimitter : ILimitter
{
    private int _maxDateDiff;
    private int _maxNumDiff;

    public AnyLimitter(int maxDateDiff, int maxNumDiff)
    {
        _maxDateDiff = maxDateDiff;
        _maxNumDiff = maxNumDiff;
    }

    public int GetOutdatedAmount(List<RestorePoint> restorePoints)
    {
        RestorePoint cur = restorePoints.Last();

        return restorePoints.Where(
            (point, i) => restorePoints.Count - i > _maxNumDiff ||
                          (cur.Date - point.Date).TotalSeconds > _maxDateDiff).Count();
    }
}