using System;
using System.IO;
using System.Collections.Generic;

namespace ExtractFoldersApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string parentFolder = Directory.GetParent(Directory.GetParent("./").FullName).FullName;
            Console.WriteLine($"Woring in: {parentFolder}");

            var dirName = parentFolder + "\\ExtractedFolder";
            string[] directories = Directory.GetDirectories(parentFolder);
            List<string> filteredDirectories = new List<string>();

            foreach (string directory in directories)
            {
                if (!(directory.Contains("ExtractFoldersApp") || directory.Contains("ExtractedFolder")))
                {
                    filteredDirectories.Add(directory);
                }
            }

            string[] filePaths = Directory.GetFiles(parentFolder, "*", SearchOption.AllDirectories);

            DirectoryInfo di = Directory.CreateDirectory(dirName);

            try
            {
                string answer = "";
                do
                {
                    Console.WriteLine($"Found {filePaths.Length} files and {filteredDirectories.Count} directories. Do you want to continue ? (Y, N)");
                    answer = Console.ReadLine();
                } while (!(answer == "Y" || answer == "N"));

                if (answer == "Y")
                {
                    foreach (string file in filePaths)
                    {
                        if (!(file.Contains("ExtractFoldersApp") || file.Contains("ExtractedFolder")))
                        {
                            Console.WriteLine($"Moving {file}...");
                            string[] splitedFile = file.Split("\\", StringSplitOptions.RemoveEmptyEntries);
                            string filename = splitedFile[splitedFile.Length - 1];
                            File.Move(file, di.FullName + "\\" + filename);
                        }
                    }
                    foreach (string direcotory in filteredDirectories)
                    {
                        Directory.Delete(direcotory);
                    }
                    Console.WriteLine("Completed!");
                    Console.ReadLine();
                }
            }
            catch (IOException) { }
        }
    }
}
