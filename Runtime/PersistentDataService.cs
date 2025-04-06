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
            
            PersistentDataServiceUtilities.LogPersistentDataServiceInitialized();
        }
        #endregion

        #region Executes
        public void Save<T>(string name, T data, bool overwrite = true)
        {
            string filePath = GetPathToFile(name);
            
            if (!overwrite && File.Exists(filePath))
                PersistentDataServiceUtilities.ThrowSaveIOException(filePath);

            string serialized = _serializer.Serialize(data);
            
            File.WriteAllText(filePath, serialized);
            
            PersistentDataServiceUtilities.LogPersistentDataServiceFileSaved(filePath);
        }
        public T Load<T>(string name)
        {
            string filePath = GetPathToFile(name);
            
            if (!File.Exists(filePath))
                PersistentDataServiceUtilities.ThrowFileNotFoundException(filePath);

            string content = File.ReadAllText(filePath);
            
            T deserialized = _serializer.Deserialize<T>(content);
            
            PersistentDataServiceUtilities.LogPersistentDataServiceFileLoaded(filePath);

            return deserialized;
        }
        public void Delete(string name)
        {
            string filePath = GetPathToFile(name);

            if (!File.Exists(filePath))
                PersistentDataServiceUtilities.ThrowFileNotFoundException(filePath);
            
            File.Delete(filePath);
            
            PersistentDataServiceUtilities.LogPersistentDataServiceFileDeleted(filePath);
        }
        public void DeleteAll()
        {
            string[] files = Directory.GetFiles(_dataPath, $"*.{_fileExtension}");

            foreach (string file in files)
                Delete(file);
        }
        #endregion
    }
}