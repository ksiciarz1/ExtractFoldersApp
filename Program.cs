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
            long size = 0;


            DirectoryInfo di;

            try
            {
                string answer = "";
                do
                {
                    foreach (string file in filePaths)
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        size += fileInfo.Length;
                    } // TODO: 
                    MyDataType fileSize = new MyDataType(size);
                    Console.WriteLine($"Found {filePaths.Length} files and {filteredDirectories.Count} directories" +
                        $" having {Math.Round(fileSize.value, 2)} {fileSize.dataTypeSize[fileSize.dataType]}. Do you want to continue ? (Y, N)");
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

        /// <summary>
        /// Small datatype making it easier to calculate file size
        /// </summary>
        struct MyDataType
        {
            public double value;
            public int dataType = 0;
            public string[] dataTypeSize = new string[] { "bytes", "kilobytes", "megabytes", "gigabytes", "terabytes" };


            /// <param name="value">Size of file</param>
            /// <param name="dataSizeType">Type of data size</param>
            public MyDataType(long value, int dataSizeType = 0)
            {
                this.value = value;
                this.dataType = dataSizeType;
                ConvertToSmallerDataTypes();
            }

            public void ConvertToSmallerDataTypes()
            {
                while (value / 1024 >= 1)
                {
                    value /= 1024;
                    dataType++;
                }
            }

            public static MyDataType ConvertToSmallerDataTypes(MyDataType dataType)
            {
                MyDataType returnDataType = dataType;
                while (returnDataType.value / 1024 >= 1)
                {
                    returnDataType.value /= 1024;
                    returnDataType.dataType++;
                }
                return returnDataType;
            }

        }
    }
}
