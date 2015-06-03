namespace NetrunnerUserApp.Interfaces
{
    public interface IoService
    {
        void SaveToFile<T>(string filePath, T obj) where T : new();
        T LoadFromFile<T>(string filePath) where T : new();
    }
}