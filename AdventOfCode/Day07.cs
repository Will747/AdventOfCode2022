namespace AdventOfCode;

public class Day07 : BaseDay
{
    private readonly string[] _input;
    private readonly Dictionary<string, List<string>> _directories;
    
    // Excluding child folder sizes
    private readonly Dictionary<string, int> _directorySizes;

    public Day07()
    {
        _input = File.ReadAllText(InputFilePath).Split("\r\n");
        _directories = new Dictionary<string, List<string>>();
        _directorySizes = new Dictionary<string, int>();
    }

    private int GetDirSize(string dir)
    {
        return _directorySizes[dir] + _directories[dir].Sum(GetDirSize);
    }
    
    public override ValueTask<string> Solve_1()
    {
        var currentDir = "/";

        for (var i = 0; i < _input.Length; i++)
        {
            var args = _input[i].Split(' ');
            if (_input[i].StartsWith("$ cd"))
            {
                if (args[2].StartsWith('/'))
                {
                    currentDir = args[2];
                }
                else if (args[2] == "..")
                {
                    var currentFolders = currentDir.Split('/');
                    currentDir = "/";
                    for (var x = 0; x < currentFolders.Length - 2; x++)
                    {
                        if (currentFolders[x].Length != 0)
                        {
                            currentDir +=  currentFolders[x] + "/";   
                        }
                    }
                }
                else
                {
                    currentDir += args[2] + "/";
                }
            }
            else if (_input[i].StartsWith("$ ls"))
            {
                var containedFolders = new List<string>();
                var totalFileSize = 0;
                
                var x = 1;
                while (i + x < _input.Length && !_input[i + x].StartsWith('$'))
                {
                    var line = _input[i + x].Split(' ');
                    if (line[0] == "dir")
                    {
                        containedFolders.Add(currentDir + line[1] + "/");
                    }
                    else
                    {
                        totalFileSize += Convert.ToInt32(line[0]);
                    }
                    x++;
                }
                
                _directorySizes.Add(currentDir, totalFileSize);
                _directories.Add(currentDir, containedFolders);

                i += x - 1;
            }
        }

        var totalSize = _directorySizes.Select(directory => GetDirSize(directory.Key)).Where(dirSize => dirSize <= 100000).Sum();
        return new ValueTask<string>(totalSize.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        const int capacity = 70000000;
        var usedSpace = GetDirSize("/");
        var remainingSpace = capacity - usedSpace;
        var spaceRequired = 30000000 - remainingSpace;

        var smallestDir = capacity;
        foreach (var dirSize in _directories.Select(directory => GetDirSize(directory.Key)).Where(dirSize => dirSize > spaceRequired && dirSize < smallestDir))
        {
            smallestDir = dirSize;
        }

        return new ValueTask<string>(smallestDir.ToString());
    }
}
