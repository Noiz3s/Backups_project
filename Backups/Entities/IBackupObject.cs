namespace Backups.Entities;

public interface IBackupObject
{
    string Name { get; }
    string Type { get; }
    IReadOnlyCollection<string> Objects { get; }
}