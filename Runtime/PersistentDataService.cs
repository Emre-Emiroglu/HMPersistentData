using System.IO;
using UnityEngine;

namespace CodeCatGames.HMPersistentData.Runtime
{
    public sealed class PersistentDataService : IPersistentDataService
    {
        #region ReadonlyFields
        private readonly ISerializer _serializer;
        private readonly string _dataPath;
        private readonly string _fileExtension;
        #endregion

        #region Getters
        private string GetPathToFile(string fileName) => Path.Combine(_dataPath, $"{fileName}.{_fileExtension}");
        #endregion

        #region Constructor
        public PersistentDataService(ISerializer serializer, string fileExtension = "dat")
        {
            _serializer = serializer;
            _dataPath = Application.persistentDataPath;
            _fileExtension = fileExtension;
        }
        #endregion

        #region Executes
        public void Save<T>(string name, T data, bool overwrite = true)
        {
            string filePath = GetPathToFile(name);
            
            if (!overwrite && File.Exists(filePath))
                throw new IOException($"File '{filePath}' already exists and overwrite is false.");

            string serialized = _serializer.Serialize(data);
            
            File.WriteAllText(filePath, serialized);
        }
        public T Load<T>(string name)
        {
            string filePath = GetPathToFile(name);
            
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File '{filePath}' not found.");

            string content = File.ReadAllText(filePath);
            
            return _serializer.Deserialize<T>(content);
        }
        public void Delete(string name)
        {
            string filePath = GetPathToFile(name);
            
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
        public void DeleteAll()
        {
            string[] files = Directory.GetFiles(_dataPath, $"*.{_fileExtension}");
            
            foreach (string file in files)
                File.Delete(file);
        }
        #endregion
    }
}