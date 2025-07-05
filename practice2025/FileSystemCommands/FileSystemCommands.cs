using System.Threading.Tasks;
using CommandLib;

namespace FileSystemCommands
{
    public class DirectorySizeCommand : ICommand
    {
        private readonly string path;

        public long DirectorySize { get; private set; }

        public DirectorySizeCommand(string path)
        {
            this.path = path;
            DirectorySize = 0;
        }

        public void Execute()
        {
            DirectorySize = 0;

            if (Directory.Exists(path))
            {
                DirectorySize = new DirectoryInfo(path)
                .GetFiles("*", SearchOption.AllDirectories)
                .Select(file => file.Length)
                .Sum();
            }
        }

    }

    public class FindFilesCommand : ICommand
    {
        private readonly string path;
        private readonly string pattern;
        public string[] FilesWithPattern { get; private set; } = Array.Empty<string>();

        public FindFilesCommand(string path, string pattern) 
        {
            this.path = path;
            this.pattern = pattern;
        }

        public void Execute()
        {
            if (Directory.Exists(path))
            {
                FilesWithPattern = Directory.GetFiles(path, pattern, SearchOption.AllDirectories);
            }
        }
    }
}
