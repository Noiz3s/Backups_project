using Backups.Extra.Entities;

namespace Backups.Extra.Models;

public class ConsoleLogger : ILogger
{
    private readonly bool _isFull;

    public ConsoleLogger(bool isFull)
    {
        _isFull = isFull;
    }

    public void CreateLog(BackupTaskExtra task)
    {
        if (_isFull)
        {
            Console.WriteLine(DateTime.Now + " " + task.BackupExtra.Log);
        }
        else
        {
            Console.WriteLine(task.BackupExtra.Log);
        }
    }
}