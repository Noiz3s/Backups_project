using Backups.Entities;
namespace Backups.Extra.Models;

public interface ILimitter
{
    int GetOutdatedAmount(List<RestorePoint> restorePoints);
}