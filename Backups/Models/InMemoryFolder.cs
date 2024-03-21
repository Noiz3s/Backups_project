using Backups.Exceptions;
namespace Backups.Models;

public class InMemoryFolder : IInMemoryFile
{
    private string _name;
    private List<IInMemoryFile> _data;

    public InMemoryFolder(string name, List<IInMemoryFile> data)
    {
        _name = name ?? throw InMemoryFileExceptions.NullNameException(
            "Tried to create Folder with null name");
        _data = data ?? throw InMemoryFileExceptions.NullDataException(
            "Tried to create Folder with null data");
    }

    public string Name => _name;
    public List<IInMemoryFile> Data => _data;

    public void AddFile(IInMemoryFile file)
    {
        if (file is null)
        {
            throw InMemoryFileExceptions.NullFileException("Tried to add null File to Folder");
        }

        _data.Add(file);
    }

    public void RemoveFile(IInMemoryFile file)
    {
        if (file is null)
        {
            throw InMemoryFileExceptions.NullFileException("Tried to remove null File from Folder");
        }

        _data.Remove(file);
    }

    public List<string> GetFolders()
    {
        return _data.OfType<InMemoryFolder>().Select(folder => ((IInMemoryFile)folder).Name).ToList();
    }

    public List<string> GetFiles()
    {
        return _data.OfType<InMemoryFile>().Select(file => ((IInMemoryFile)file).Name).ToList();
    }
}