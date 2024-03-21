using Backups.Extra.Entities;

namespace Backups.Extra.Models;

public class FileLogger : ILogger
{
    private readonly string _path;
    private readonly bool _isFull;

    public FileLogger(string path, bool isFull)
    {
        _path = path;
        _isFull = isFull;
        File.Create(_path).Dispose();
    }

    public void CreateLog(BackupTaskExtra task)
    {
        var writer = new StreamWriter(_path, true);
        if (_isFull)
        {
            writer.WriteLine(DateTime.Now + " " + task.BackupExtra.Log);
        }
        else
        {
            writer.WriteLine(task.BackupExtra.Log);
        }

        writer.Close();
    }
}