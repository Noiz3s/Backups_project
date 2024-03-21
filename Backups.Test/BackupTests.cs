using Backups.Algoritms;
using Backups.Entities;
using Backups.Models;
using Xunit;
namespace Backups.Test;

public class BackupTests
{
    [Fact]
    public void TestCaseOne()
    {
        int id = 1;

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
        var repo = new InMemoryRepository(folder, files);
        var algo = new SplitStorage(repo);

        var task = new BackupTask(new List<string>(), " ", id, repo);

        task.ExecuteTask(algo, files);

        files.Remove(folder2);

        task.ExecuteTask(algo, files);
        var temp = (repo.Folder.Data[0] as InMemoryFolder) !;
        Assert.Equal(2, temp.Data.Count);

        Assert.Equal(2, (temp.Data[0] as InMemoryFolder) !.Data.Count);
        Assert.Single((temp.Data[1] as InMemoryFolder) !.Data);
    }

    [Fact]
    public void TestCasetwo()
    {
        int id = 1;

        var folder = new InMemoryFolder("Repo Folder", new List<IInMemoryFile>());

        var filesFolder = new InMemoryFolder("Folder 1", new List<IInMemoryFile>());
        var file1 = new InMemoryFile("File 1", "SomeData");
        var file2 = new InMemoryFile("File 2", "SomeData 2");
        filesFolder.AddFile(file1);
        filesFolder.AddFile(file2);
        var files = new List<InMemoryFolder> { filesFolder };

        var repo = new InMemoryRepository(folder, files);
        var algo = new SingleStorage(repo);

        var task = new BackupTask(new List<string>(), " ", id, repo);

        task.ExecuteTask(algo, files);
        Assert.Single(repo.Folder.Data);

        var temp = (repo.Folder.Data[0] as InMemoryFolder) !;
        Assert.Single(temp.Data);

        temp = (temp.Data[0] as InMemoryFolder) !;
        Assert.Single(temp.Data);

        temp = (temp.Data[0] as InMemoryFolder) !;
        Assert.Equal(2, temp.Data.Count);
    }
}