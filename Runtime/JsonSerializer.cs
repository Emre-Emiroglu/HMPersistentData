using Newtonsoft.Json;

namespace CodeCatGames.HMPersistentData.Runtime
{
    public sealed class JsonSerializer : ISerializer
    {
        #region Executes
        public string Serialize<T>(T obj) => JsonConvert.SerializeObject(obj);
        public T Deserialize<T>(string str) => JsonConvert.DeserializeObject<T>(str);
        #endregion
    }
}