using System.IO;
using Client;
using Newtonsoft.Json;
using NUnit.Framework;

namespace TestProject1
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            FolderInfo calculatedHierarchy = Program.DirectionDiscovery(@"../../../UnitTests/TestFolder");
            string testString = JsonConvert.SerializeObject(calculatedHierarchy);
            
            string actualString = File.ReadAllText(@"../../fileManifest.json");


            Assert.AreEqual(actualString, testString);
        }
    }
}