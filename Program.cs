using System;
using System.IO;

namespace ExtractFoldersApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string parentFolder = Directory.GetParent(Directory.GetParent("./").FullName).FullName;
            Console.WriteLine($"Woring in: {parentFolder}");

            var dirName = parentFolder + "\\ExtractedFolder";
            string[] filePaths = Directory.GetFiles(parentFolder, "*", SearchOption.AllDirectories);

            DirectoryInfo di = Directory.CreateDirectory(dirName);

            try
            {
                string answer = "";
                do
                {
                    Console.WriteLine($"Found {filePaths.Length} files. Do you want to continue ? (Y, N)");
                    answer = Console.ReadLine();
                } while (!(answer == "Y" || answer == "N"));

                if (answer == "Y")
                {
                    foreach (string file in filePaths)
                    {
                        Console.WriteLine($"Moving {file}...");
                        File.Move(file, di.Name);
                        Console.WriteLine($"Done!");
                    }
                }
            }
            catch (IOException) { }
        }
    }
}
