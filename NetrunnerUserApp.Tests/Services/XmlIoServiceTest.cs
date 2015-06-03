using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetrunnerUserApp.Services;
using System;
using System.Collections.Generic;
using System.IO;

namespace NetrunnerUserApp.Tests.Services
{
    [TestClass]
    public class XmlIoServiceTests
    {
        private XmlIoService _ioService;

        [TestInitialize]
        public void Init()
        {
            _ioService = new XmlIoService();
        }

        [TestMethod]
        public void XmlIoService_SaveToNonExistentDirectory_DirectoryCreatedAndFileSaved()
        {
            var obj = new TestObject { Name = "myTest" };
            var filePath = Path.Combine(Environment.CurrentDirectory, "newDir\\testobject.xml");

            _ioService.SaveToFile(filePath, obj);
            var loadedObj = _ioService.LoadFromFile<TestObject>(filePath);

            Assert.AreEqual("myTest", loadedObj.Name);

            File.Delete(filePath);
            Directory.Delete(Path.GetDirectoryName(filePath));
        }

        [TestMethod, TestCategory("Unit Test")]
        public void XmlIoService_SaveAndLoadObject_ObjectCorrect()
        {
            var obj = new TestObject { Name = "myTest" };
            var currentDir = Environment.CurrentDirectory;
            var filePath = Path.Combine(currentDir, "testobject.xml");

            _ioService.SaveToFile(filePath, obj);
            var loadedObj = _ioService.LoadFromFile<TestObject>(filePath);

            Assert.AreEqual("myTest", loadedObj.Name);

            File.Delete(filePath);
        }

        [TestMethod, TestCategory("Unit Test")]
        public void XmlIoService_SaveAndLoadObjectList_ListCorrect()
        {
            var obj = new TestObject { Name = "myTest" };
            var obj2 = new TestObject { Name = "myTest2" };
            var list = new List<TestObject> { obj, obj2 };
            var filePath = Path.Combine(Environment.CurrentDirectory, "testobject.xml");

            _ioService.SaveToFile(filePath, list);
            var loadedList = _ioService.LoadFromFile<List<TestObject>>(filePath);

            Assert.AreEqual("myTest", loadedList[0].Name);
            Assert.AreEqual("myTest2", loadedList[1].Name);

            File.Delete(filePath);
        }

        [Serializable]
        public class TestObject
        {
            public string Name { get; set; }
        }
    }
}