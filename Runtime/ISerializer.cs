namespace CodeCatGames.HMPersistentData.Runtime
{
    public interface ISerializer
    {
        public string Serialize<T>(T obj);
        public T Deserialize<T>(string str);
    }
}