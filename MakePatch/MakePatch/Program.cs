using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace MakePatch
{
    class Program
    {
        static void Main(string[] args)
        {
            IDictionary<string, byte[]> prevFilesHashes = GetFiles(args[0]);
            IDictionary<string, byte[]> newFilesHashes = GetFiles(args[1]);

            IEnumerable<string> result = CompareVersions(prevFilesHashes, newFilesHashes);

            CopyFiles(result, args[1], args[2]);
        }

        /// <summary>
        /// копирует измененные файлы 
        /// </summary>
        /// <param name="difFileNames">перечень файлов для копирования</param>
        /// <param name="pathFrom">откуда копировать</param>
        /// <param name="pathTo">куда копировать</param>
        private static void CopyFiles(IEnumerable<string> difFileNames, string pathFrom, string pathTo)
        {
            try
            {
                HashSet<string> files = new HashSet<string>(difFileNames);
                var fileNames = Directory.GetFiles(pathFrom);
                foreach (string fileName in fileNames)
                {
                    if (files.Contains(fileName))
                    {
                        var to = $"{pathTo}\\{new FileInfo(fileName).Name}";
                        Console.WriteLine($"CopyFiles:Processing file: {fileName} to {to}");
                        File.Copy(fileName, to, true);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// сравнивает хеши файлов разных версий
        /// </summary>
        /// <param name="prevFilesHashes">прошлая версия</param>
        /// <param name="newFilesHashes">новая версия</param>
        /// <returns>измененные файлы</returns>
        private static IEnumerable<string> CompareVersions(IDictionary<string, byte[]> prevFilesHashes, IDictionary<string, byte[]> newFilesHashes)
        {
            List<string> result = new List<string>();
            try
            {
                // справочники с короткими именами файлов
                Dictionary<string, string> prevFilesList = new Dictionary<string, string>(from item in prevFilesHashes select new KeyValuePair<string, string>(new FileInfo(item.Key).Name, item.Key));
                Dictionary<string, string> newFilesList = new Dictionary<string, string>(from item in newFilesHashes select new KeyValuePair<string, string>(new FileInfo(item.Key).Name, item.Key));

                foreach (var newItem in newFilesList)
                {
                    Console.WriteLine($"CompareVersions:Processing file: {newItem.Key}");
                    // файл новый
                    if (!prevFilesList.ContainsKey(newItem.Key))
                    {
                        result.Add(newItem.Value);
                        Console.WriteLine($"\t added");
                        continue;
                    }

                    // немного мутно из-за полного и короткого путей файлов
                    var prevItemData = prevFilesHashes[prevFilesList[newItem.Key]];
                    var newItemData = newFilesHashes[newItem.Value];

                    // размер хешей разный
                    if (prevItemData.Length != newItemData.Length)
                    {
                        result.Add(newItem.Value);
                        Console.WriteLine($"\t added");
                        continue;
                    }

                    // побайтное сравнение хешей
                    for (int i = 0; i < newItemData.Length; i++)
                    {
                        // хеши отличаются
                        if (newItemData[i] != prevItemData[i])
                        {
                            result.Add(newItem.Value);
                            Console.WriteLine($"\t added");
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return result;
        }

        /// <summary>
        /// Получаем хеши файлов из каталога
        /// </summary>
        /// <param name="path">каталог</param>
        /// <returns>справочник [полный путь файла, хеш]</returns>
        private static IDictionary<string, byte[]> GetFiles(string path)
        {
            Console.WriteLine($"GetFiles: Get files hash from {path}");
            Dictionary<string, byte[]> filesHashes = new Dictionary<string, byte[]>();
            try
            {
                var extentions = new [] {".dll", ".exe" };
                var fileNames = Directory.GetFiles(path).Where(file => extentions.Any(file.ToLower().EndsWith)).ToList();
                SHA512 hashCalculator = new SHA512Managed();

                foreach (string fileName in fileNames)
                {
                    byte[] fileBytes = File.ReadAllBytes(fileName);
                    byte[] fileHash = hashCalculator.ComputeHash(fileBytes);
                    filesHashes.Add(fileName, fileHash);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return filesHashes;
        }
    }
}
