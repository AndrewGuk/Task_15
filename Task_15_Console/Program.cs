using System;
using System.IO;
using System.IO.Compression;

namespace Task_15
{
    class Program
    {
        static void Main(string[] args)
        {
            const int WARN_WIN32_FILE_EXISTS = unchecked((int)0x80070050);
            string patch = @"D:\tes33t";
            DirectoryInfo dirInfo = new DirectoryInfo(patch);

            string mask = "*.TXT";

            try
            {
                // Ищем файлы в корневой директории.
                getFiles(dirInfo);
                // Рекурсивный поиск по поддиректориям.
                getChildDirectories(dirInfo);
                Console.WriteLine("DONE");
            }

            catch (IOException ex) when (ex.HResult == WARN_WIN32_FILE_EXISTS)
            {
            }
            





            void getChildDirectories(DirectoryInfo rootDirectory)
            {
                foreach (DirectoryInfo directory in rootDirectory.GetDirectories())
                {
                    try
                    {
                        getFiles(directory);
                        getChildDirectories(directory);
                    }
                    catch (IOException ex) when (ex.HResult == WARN_WIN32_FILE_EXISTS)
                    {
                    }
                }
            }
            void getFiles(DirectoryInfo directory)
            {
                var files = Directory.EnumerateFiles(directory.FullName, mask, SearchOption.TopDirectoryOnly);
                foreach (var file in files)
                {
                    //ZipFile.CreateFromDirectory(directory.FullName, file.Replace(".TXT", ".zip"), CompressionLevel.Fastest, true);
                    ZipFile.CreateFromDirectory(directory.FullName, file.Replace(mask.Trim('*'), ".zip"), CompressionLevel.Fastest, true);

                }
            }
        }

    }
}