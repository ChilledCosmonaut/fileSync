using System;
using System.IO;
using Newtonsoft.Json;

namespace ConsoleApplication1
{
    public static class Program
    {
        private static int uidCounter;
        public static void Main(string[] args)
        {
            //string games = @"D:\Games";
            /*string uniStuff = @"../../../UnitTests/TestFolder";//C:\Users\GoPJo\Desktop\UniStuff";
            FolderInfo folderHierarchy = FolderParser.DirectionDiscovery(uniStuff);*/
            string firstHierarchy = File.ReadAllText(@"./fileManifest.json");
            FolderInfo folderHierarchy = JsonConvert.DeserializeObject<FolderInfo>(firstHierarchy);
            Console.WriteLine(folderHierarchy.subfolderInfos[0].subfolderInfos.Count);
            /*string output = JsonConvert.SerializeObject(folderHierarchy);
            Console.WriteLine(uidCounter);
            File.WriteAllText(@".\fileManifest.json", output);*/
        }
        
        private static int GenerateUniqueId()
        {
            uidCounter %= Int32.MaxValue;
            return uidCounter++;
        }
    }
}