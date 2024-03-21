using Backups.Entities;

namespace Backups.Extra.Models;

public class NumberLimitter : ILimitter
{
    private int _maxNumberDiff;

    public NumberLimitter(int maxNumberDiff)
    {
        _maxNumberDiff = maxNumberDiff;
    }

    public int GetOutdatedAmount(List<RestorePoint> restorePoints)
    {
        int ans = restorePoints.Count - _maxNumberDiff;
        if (ans < 0)
        {
            ans = 0;
        }

        return ans;
    }
}