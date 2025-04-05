using UnityEngine;

namespace CodeCatGames.HMPersistentData.Runtime
{
    public sealed class JsonSerializer : ISerializer
    {
        #region Executes
        public string Serialize<T>(T obj) => JsonUtility.ToJson(obj, true);
        public T Deserialize<T>(string str) => JsonUtility.FromJson<T>(str);
        #endregion
    }
}