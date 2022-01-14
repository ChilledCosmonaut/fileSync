﻿using System.Collections.Generic;

namespace ConsoleApplication1
{
    public class FolderInfo
    {
        public string folderName;
                                 
        public int uid;

        public bool deleted = false;
        
        public bool projectFolder = false;
                                 
        public string gitUrl;

        public List<FolderInfo> subfolderInfos = new List<FolderInfo>();

        public List<FileInfo> fileInfos = new List<FileInfo>();

        
    }
}