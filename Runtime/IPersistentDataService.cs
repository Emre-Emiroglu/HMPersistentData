namespace CodeCatGames.HMPersistentData.Runtime
{
    public interface IPersistentDataService
    {
        public void Save<T>(string name, T data, bool overwrite = true);
        public T Load<T>(string name);
        public void Delete(string name);
        public void DeleteAll();
    }
}