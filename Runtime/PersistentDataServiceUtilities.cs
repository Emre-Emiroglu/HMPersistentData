using System.IO;
using UnityEngine;

namespace CodeCatGames.HMPersistentData.Runtime
{
    public static class PersistentDataServiceUtilities
    {
        #region Fields
        private static IPersistentDataService _persistentDataService;
        private static bool _isInitialized;
        #endregion

        #region Core
        public static void Initialize(SerializerType serializerType, string fileExtension, string key, string iv)
        {
            _persistentDataService = serializerType switch
            {
                SerializerType.Json => new PersistentDataService(new JsonSerializer(), fileExtension),
                SerializerType.EncryptedJson => new PersistentDataService(new EncryptedJsonSerializer(key, iv),
                    fileExtension),
                _ => new PersistentDataService(new JsonSerializer(), fileExtension)
            };

            _isInitialized = true;
        }
        private static void EnsureInitialized()
        {
            if (_isInitialized)
                return;

            _persistentDataService = new PersistentDataService(new JsonSerializer(), "dat");
            
            _isInitialized = true;
        }
        #endregion

        #region Executes
        public static void Save<T>(string name, T data, bool overwrite = true)
        {
            EnsureInitialized();
            
            _persistentDataService.Save(name, data, overwrite);
        }
        public static T Load<T>(string name)
        {
            EnsureInitialized();
            
            return _persistentDataService.Load<T>(name);
        }
        public static void Delete(string name)
        {
            EnsureInitialized();
            
            _persistentDataService.Delete(name);
        }
        public static void DeleteAll()
        {
            EnsureInitialized();
            
            _persistentDataService.DeleteAll();
        }
        public static void LogPersistentDataServiceInitialized() => Debug.Log("Persistent Data Service initialized");
        public static void LogPersistentDataServiceFileSaved(string filePath) => Debug.Log($"File '{filePath}' saved");
        public static void LogPersistentDataServiceFileLoaded(string filePath) =>
            Debug.Log($"File '{filePath}' loaded");
        public static void LogPersistentDataServiceFileDeleted(string filePath) =>
            Debug.Log($"File '{filePath}' deleted");
        public static void ThrowSaveIOException(string filePath) =>
            throw new IOException($"File '{filePath}' already exists and overwrite is false.");
        public static void ThrowFileNotFoundException(string filePath) =>
            throw new FileNotFoundException($"File '{filePath}' not found.");
        #endregion
    }
}