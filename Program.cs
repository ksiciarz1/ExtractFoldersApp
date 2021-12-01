using System;
using System.IO;

namespace ExtractFoldersApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var dirName = $@"./Excrated Folders";
            string[] filePaths = Directory.GetFiles(@"./", "*.bmp", SearchOption.AllDirectories);

            DirectoryInfo di = Directory.CreateDirectory(dirName);

            try
            {
                System.Console.WriteLine($"Found {filePaths.Length} files. Do you want to continue ? (Y, N)");
                string answer = System.Console.ReadLine();

                foreach (string file in filePaths)
                {
                    Console.WriteLine($"Moving {file}...");
                    File.Move(file, di.Name);
                    Console.WriteLine($"Done!");
                }
            }
            catch (IOException) { }
        }
    }
}
