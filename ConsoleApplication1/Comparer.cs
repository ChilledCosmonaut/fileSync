using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    public class Comparer
    {
        private static List<FolderInfo> directoriesToDownload = new List<FolderInfo>();
        private static List<FileInfo> filesToDownload = new List<FileInfo>();

        public FolderInfo GetDifferenceToFolderInfo(FolderInfo main, FolderInfo comparer)
        {
            Dictionary<string, FolderInfo> comparerSubFolders = comparer.subfolderInfos.ToDictionary(m => m.folderName);
            foreach (FolderInfo subfolder in main.subfolderInfos)
            {
                if (!comparerSubFolders.ContainsKey(subfolder.folderName))
                {
                    directoriesToDownload.Add(subfolder);
                }
            }

            Dictionary<string, FileInfo> comparerFiles = comparer.fileInfos.ToDictionary(m => m.fileName);
            foreach (FileInfo file in main.fileInfos)
            {
                if (comparerFiles[file.fileName].lastTimeEdited.CompareTo(file.lastTimeEdited) <= 0)
                {
                    filesToDownload.Add(file);
                }
            }
        }
        
        public FolderInfo MergeTwoRootFolderInfos(FolderInfo first, FolderInfo second)
        {
            FolderInfo result = new FolderInfo();
            
            return result;
        }

        private FolderInfo MergeFolderInfos(FolderInfo first, FolderInfo second)
        {
            
        }
    }
}