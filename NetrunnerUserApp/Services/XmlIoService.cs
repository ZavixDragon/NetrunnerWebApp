using NetrunnerUserApp.Interfaces;
using System.IO;
using System.Xml.Serialization;

namespace NetrunnerUserApp.Services
{
    public class XmlIoService : IoService
    {
        public void SaveToFile<T>(string filePath, T obj) where T : new()
        {
            CreateDirectoryIfDoesNotExist(filePath);
            using (var writer = new StreamWriter(filePath, append: false))
                new XmlSerializer(typeof(T)).Serialize(writer, obj);
        }

        public T LoadFromFile<T>(string filePath) where T : new()
        {
            using (var reader = new StreamReader(filePath))
                return (T)(new XmlSerializer(typeof(T)).Deserialize(reader));
        }

        private void CreateDirectoryIfDoesNotExist(string filePath)
        {
            var dir = Path.GetDirectoryName(filePath);
            if (dir != null && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }
    }
}