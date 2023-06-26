using System.IO;

namespace Backend;

public class EnvironmentVariable
{
    private string _path;
    public EnvironmentVariable(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException();

    }

    public T Get<T>(string target)
    {
        return default;
    }
}