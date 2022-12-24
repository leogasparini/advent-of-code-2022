using System.Collections.Immutable;

namespace AdventOfCode.Days;

public class Day7 : AdventOfCodeDay
{
    protected override int GetDay() => 7;

    protected override string GetTask1Solution()
    {
        int freeableSpace = 0;
        DeviceItem currentItem = BuildFileSystem();

        ComputeFreeableSpace();
        
        return freeableSpace.ToString();

        void ComputeFreeableSpace()
        {
            int dirSize = currentItem.GetTotalSize();
            if (dirSize <= 100000)
            {
                freeableSpace += dirSize;
            }

            foreach (DeviceItem directory in currentItem.Directories)
            {
                currentItem = directory;
                ComputeFreeableSpace();
            }
        }
    }

    protected override string GetTask2Solution()
    {
        const int diskSize = 70_000_000;
        const int requiredUnusedSpace = 30_000_000;
        DeviceItem root = BuildFileSystem();
        int usedSpace = root.GetTotalSize();
        int spaceToFreeUp = requiredUnusedSpace - (diskSize - usedSpace);
        DeviceItem deletableDir = root;
        int deletableDirSize = deletableDir.GetTotalSize();
        DeviceItem currentDir = root;

        FindDeletableDirectory();
        
        return deletableDirSize.ToString();
        
        void FindDeletableDirectory()
        {
            foreach (DeviceItem directory in currentDir.Directories)
            {
                int totalSize = directory.GetTotalSize();
                if (totalSize >= spaceToFreeUp && totalSize <= deletableDirSize)
                {
                    deletableDir = directory;
                    deletableDirSize = deletableDir.GetTotalSize();
                }

                currentDir = directory;
                FindDeletableDirectory();
            }
        }
    }

    private DeviceItem BuildFileSystem()
    {
        string inputPath = Path.Combine("Assets", "day7.txt");
        DeviceItem root = new DeviceItem { Name = "/" };
        DeviceItem currentItem = root;

        foreach (string line in File.ReadLines(inputPath))
        {
            if (IsCommand(line))
                ExecCommand(line);
            else
                ParseOutput(line);
        }

        bool IsCommand(string line) => line.StartsWith("$");
        bool IsCdCommand(string line) => line.StartsWith("$ cd");
        bool IsDir(string line) => line.StartsWith("dir");

        void ExecCommand(string line)
        {
            if (IsCdCommand(line))
            {
                ExecCdCommand(line);
            }
        }

        void ExecCdCommand(string line)
        {
            string dirName = line.Replace("$ cd ", "", StringComparison.InvariantCulture);

            currentItem = dirName switch
            {
                "/" => root,
                ".." => currentItem.Parent ?? root,
                _ => currentItem.Directories.First(d => d.Name.Equals(dirName, StringComparison.InvariantCulture))
            };
        }

        void ParseOutput(string line)
        {
            if (IsDir(line))
            {
                string dirName = line.Replace("dir ", "", StringComparison.InvariantCulture);
                currentItem.AddChild(new DeviceItem
                {
                    Name = dirName,
                    Size = 0,
                    IsDir = true
                });
            }
            else
            {
                string fileName = line.Split(' ').Last();
                int fileSize = int.Parse(line.Split(' ').First());
                currentItem.AddChild(new DeviceItem
                {
                    Name = fileName,
                    Size = fileSize
                });
            }
        }

        return root;
    }
}

internal sealed record DeviceItem
{
    public bool IsDir { get; init; }
    public string Name { get; init; } = default!;
    public int Size { get; init; }
    public HashSet<DeviceItem> Children { get; } = new();
    public ImmutableHashSet<DeviceItem> Directories => Children.Where(c => c.IsDir).ToImmutableHashSet();
    public DeviceItem? Parent { get; private set; }

    public void AddChild(DeviceItem item)
    {
        item.Parent = this;
        Children.Add(item);
    }

    public int GetTotalSize()
    {
        int filesSize = Size;
        
        foreach (DeviceItem child in Children)
        {
            filesSize += child.GetTotalSize();
        }
        
        return filesSize;
    }
}