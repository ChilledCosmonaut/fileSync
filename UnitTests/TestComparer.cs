using System.IO;
using ConsoleApplication1;
using Newtonsoft.Json;
using NUnit.Framework;

namespace TestProject1
{
    [TestFixture]
    public class TestComparer
    {
        [Test]
        public void TestGetFilesToDownloadAsJson()
        {
            string clientHierarchy = File.ReadAllText(@"../../fileManifest.json");
            FolderInfo clientInfoHierarchy = JsonConvert.DeserializeObject<FolderInfo>(clientHierarchy);
            string serverHierarchy = File.ReadAllText(@"../../serverFileManifest.json");
            FolderInfo serverInfoHierarchy = JsonConvert.DeserializeObject<FolderInfo>(serverHierarchy);
            string expectedDownloadHierarchy = File.ReadAllText(@"../../expectedDowloadManifest.json");

            DownloadInfo calculatedDownloadInfoHierarchy = Comparer.GetDifferenceToFolderInfo(clientInfoHierarchy, serverInfoHierarchy);
            string calculatedDownloadHierarchy = JsonConvert.SerializeObject(calculatedDownloadInfoHierarchy);

            Assert.AreEqual(expectedDownloadHierarchy, calculatedDownloadHierarchy);
        }
    }
}