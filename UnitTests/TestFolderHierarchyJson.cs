using System.IO;
using ConsoleApplication1;
using Newtonsoft.Json;
using NUnit.Framework;

namespace TestProject1
{
    [TestFixture]
    public class TestFolderParser
    {
        [Test]
        public void TestFolderHierarchyJson()
        {
            FolderInfo calculatedHierarchy = FolderParser.DirectionDiscovery(@"../../../UnitTests/TestFolder");
            string testString = JsonConvert.SerializeObject(calculatedHierarchy);
            
            string actualString = File.ReadAllText(@"../../fileManifest.json");


            Assert.AreEqual(actualString, testString);
        }
    }
}