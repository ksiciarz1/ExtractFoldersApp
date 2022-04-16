using System;
using System.IO;
using System.Collections.Generic;

namespace ExtractFoldersApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string parentFolder = Directory.GetCurrentDirectory();
            Console.WriteLine($"Woring in: {parentFolder}");

            var dirName = parentFolder + "\\ExtractedFolder";
            string[] directories = Directory.GetDirectories(parentFolder);
            List<string> filteredDirectories = new List<string>();

            foreach (string dir in directories)
            {
                if (!(dir.Contains("ExtractFoldersApp") || dir.Contains("ExtractedFolder")))
                {
                    filteredDirectories.Add(dir);
                }
            }

            string[] filePaths = Directory.GetFiles(parentFolder, "*", SearchOption.AllDirectories);

            DirectoryInfo di;

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
                    di = Directory.CreateDirectory(dirName);
                    foreach (string fileString in filePaths)
                    {
                        if (!(fileString.Contains("ExtractFoldersApp") || fileString.Contains("ExtractedFolder")))
                        {
                            Console.WriteLine($"Moving {fileString}...");
                            string[] splitedFile = fileString.Split("\\", StringSplitOptions.RemoveEmptyEntries);
                            string fileName = splitedFile[splitedFile.Length - 1];
                            File.Move(fileString, di.FullName + "\\" + fileName);
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
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
