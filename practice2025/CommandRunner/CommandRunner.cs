using CommandLib;
using System.Reflection;
using FileSystemCommands;

namespace CommandRunner
{
    public class CommandRunner
    {
        static void Main()
        {
            var path_1 = @"C:\Users\Елена\задание 8\practice2025\";
            var directory_size_test = new DirectorySizeCommand(path_1);
            directory_size_test.Execute();

            Console.WriteLine($"Размер директории: {directory_size_test.DirectorySize}");

            var path_2 = @"C:\Users\Елена\задание 8\practice2025\CommandRunner";
            var pattern_2 = "*.cs";
            var find_files_test = new FindFilesCommand(path_2, pattern_2);
            find_files_test.Execute();

            if (find_files_test.FilesWithPattern.Length != 0)
            {
                Console.WriteLine("Найденные файлы с выбранной маской:");
                foreach (var file_with_pattern in find_files_test.FilesWithPattern) Console.WriteLine(file_with_pattern);
            }
        }

    }
}
