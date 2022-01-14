using System.IO;
using Client;
using Newtonsoft.Json;
using NUnit.Framework;

namespace TestProject1
{
    [TestFixture]
    public class TestFolderParser
    {
        [Test]
        public void TestGetFolderAsJson()
        {
            FolderInfo calculatedHierarchy = FolderParser.DirectionDiscovery(@"../../../UnitTests/TestFolder");
            string testString = JsonConvert.SerializeObject(calculatedHierarchy);
            
            string actualString = File.ReadAllText(@"../../fileManifest.json");


            Assert.AreEqual(actualString, testString);
        }
    }
}