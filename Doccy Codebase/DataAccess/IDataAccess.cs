
namespace DataAccessLibrary
{
    public interface IDataAccess
    {
        string ConnectionString { get; set; }
        Task<List<T>> LoadData<T, U>(string sql, U parameters);
        Task<T?> LoadSingleRowAsync<T, U>(string sql, U parameters);
        Task SaveData<T>(string sql, T parameters);
    }
}