using Backups.Entities;
using Backups.Models;

namespace Backups.Algoritms;

public interface IAlgo
{
    void Save(BackupTask task);

    void Save(BackupTask task, IReadOnlyCollection<InMemoryFolder> files);
}