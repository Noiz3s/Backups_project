using System.Globalization;
using Backups.Algoritms;
using Backups.Entities;
using Backups.Extra.Entities;
using Backups.Extra.Models;
using Backups.Extra.Services;
using Backups.Models;
using Xunit;
namespace Backups.Extra.Test;

public class BackupExtraTests
{
    [Fact]
    public void RemovingRestorePointsCorrectly()
    {
        var limitter = new NumberLimitter(1);
        var logger = new FileLogger("C:\\TempLogs\\Logger.txt", true);
        var manager = new ConditionManager("C:\\ConditionManager");
        var service = new BackupExtraService(limitter, false, logger, manager);

        var file1 = new InMemoryFile("File 1", "SomeData");
        var file2 = new InMemoryFile("File 2", "SomeData 2");
        var folder1 = new InMemoryFolder("Folder 1", new List<IInMemoryFile> { file1 });
        var folder2 = new InMemoryFolder("Folder 2", new List<IInMemoryFile> { file2 });

        var files = new List<InMemoryFolder>
        {
            folder1,
            folder2,
        };

        var folder = new InMemoryFolder("Repo Folder", new List<IInMemoryFile>());
        var repo = new InMemoryRepositoryExtra(folder, files);
        var algo = new SplitStorage(repo);

        var task = new BackupTaskExtra(new List<string>(), " ", 1, repo);

        service.ExecuteBackUpTask(task, algo, files);
        files.Remove(folder2);

        service.ExecuteBackUpTask(task, algo, files);
        var temp = (repo.Folder.Data[0] as InMemoryFolder) !;

        Assert.Single(temp.Data);
    }

    [Fact]
    public void MergingRestorePointsCorrectly()
    {
        var limitter = new NumberLimitter(1);
        var logger = new FileLogger("C:\\TempLogs\\Logger.txt", true);
        var manager = new ConditionManager("C:\\ConditionManager");
        var service = new BackupExtraService(limitter, true, logger, manager);

        var file1 = new InMemoryFile("File 1", "SomeData");
        var file2 = new InMemoryFile("File 2", "SomeData 2");
        var folder1 = new InMemoryFolder("Folder 1", new List<IInMemoryFile> { file1 });
        var folder2 = new InMemoryFolder("Folder 2", new List<IInMemoryFile> { file2 });

        var files = new List<InMemoryFolder>
        {
            folder1,
            folder2,
        };

        var folder = new InMemoryFolder("Repo Folder", new List<IInMemoryFile>());
        var repo = new InMemoryRepositoryExtra(folder, files);
        var algo = new SplitStorage(repo);

        var task = new BackupTaskExtra(new List<string>(), " ", 1, repo);

        service.ExecuteBackUpTask(task, algo, files);
        files.Remove(folder2);

        service.ExecuteBackUpTask(task, algo, files);
        var temp = (repo.Folder.Data[0] as InMemoryFolder) !;

        Assert.Single(temp.Data);
    }
}