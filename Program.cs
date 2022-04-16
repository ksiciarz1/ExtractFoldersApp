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

            // Gets all files across Directories
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
                    // Creates Directory if it doesn't exit
                    di = Directory.Exists(dirName) ? new DirectoryInfo(dirName) : Directory.CreateDirectory(dirName);

                    // For every file across Directories
                    foreach (string fileString in filePaths)
                    {
                        try
                        {
                            if (!(fileString.Contains("ExtractFoldersApp") || fileString.Contains("ExtractedFolder")))
                            {
                                Console.WriteLine($"Moving {fileString}...");

                                // Getting Name to keep it the same after moving
                                string[] splitedFile = fileString.Split("\\", StringSplitOptions.RemoveEmptyEntries);
                                string fileName = splitedFile[splitedFile.Length - 1];
                                File.Move(fileString, di.FullName + "\\" + fileName);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            Console.Read();
                        }
                    }

                    // Deleting Directories that are left behind
                    foreach (string direcotory in filteredDirectories)
                    {
                        try
                        {
                            Directory.Delete(direcotory);
                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex);
                            Console.Read();
                        }
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
