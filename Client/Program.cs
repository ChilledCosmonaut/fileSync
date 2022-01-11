using System;
using System.IO;
using Newtonsoft.Json;

namespace ConsoleApplication1
{
    internal static class Program
    {
        private static int uidCounter;
        public static void Main(string[] args)
        {
            //string games = @"D:\Games";
            string uniStuff = @"C:\Users\GoPJo\Desktop\UniStuff";
            FolderInfo folderHierarchy = DirectionDiscovery(uniStuff);
            string output = JsonConvert.SerializeObject(folderHierarchy);
            Console.WriteLine(uidCounter);
            File.WriteAllText(@".\fileManifest.json", output);
        }

        private static FolderInfo DirectionDiscovery(string directionPath)
        {
            string[] discoveredDirectories = Directory.GetDirectories(directionPath);

            var currentFolder = new FolderInfo 
            {
                folderName = directionPath,
                uid = GenerateUniqueId()
            };

            bool folderLegal = true;
            
            foreach (string directory in discoveredDirectories)
            {
                string[] uriParts = directory.Split('\\');
                string nextDirectory = uriParts[uriParts.Length - 1];
                
                if (nextDirectory.Equals(".git"))
                {
                    Console.WriteLine(directory);
                    folderLegal = false;
                    break;
                }
            }

            if (folderLegal)
            {
                foreach (string directory in discoveredDirectories)
                {
                    currentFolder.subfolderInfos.Add(DirectionDiscovery(directory));
                }
                string[] discoveredFiles = Directory.GetFiles(directionPath);

                foreach (var file in discoveredFiles)
                {
                    var currentFile = new FileInfo
                    {
                        fileName = file,
                        lastTimeEdited = Directory.GetLastWriteTime(file),
                        uid = GenerateUniqueId()
                    };
                    currentFolder.fileInfos.Add(currentFile);
                }
            }
            else
            {
                currentFolder.projectFolder = true;
                string gitConfig = File.ReadAllText($@"{directionPath}\.git\config");
                string[] configSections = gitConfig.Split('[');
                foreach (string section in configSections)
                {
                    if (section.Contains("\"origin\""))
                    {
                        var gitOrigin = section;
                        Console.WriteLine(section);
                        string[] delimiters = { "\\v", "\v", "\r", "\n" };
                        string[] originLines = gitOrigin.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string line in originLines)
                        {
                            if (line.Contains("url "))
                            {
                                string[] parts = line.Split(' ');
                                currentFolder.gitUrl = parts[2];
                                Console.WriteLine(parts[2]);
                                break;
                            }
                        }
                        break;
                    }
                }
                
                //currentFolder.gitUrl
            }

            return currentFolder;
        }

        private static int GenerateUniqueId()
        {
            uidCounter %= Int32.MaxValue;
            return uidCounter++;
        }
    }
}