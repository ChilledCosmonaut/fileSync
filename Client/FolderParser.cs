using System;
using System.IO;

namespace ConsoleApplication1
{
    public class FolderParser
    {
        public static FolderInfo DirectionDiscovery(string directionPath)
        {
            string[] discoveredDirectories = Directory.GetDirectories(directionPath);

            var currentFolder = new FolderInfo
            {
                folderName = directionPath
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
                        lastTimeEdited = Directory.GetLastWriteTime(file)
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
                        string[] delimiters =
                        {
                            "\\v", "\v", "\r", "\n"
                        };
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
    }
}