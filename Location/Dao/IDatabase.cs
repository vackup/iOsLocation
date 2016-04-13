using System.Threading;
using System.Threading.Tasks;
using SQLite.Net.Async;

namespace Location.Dao
{
    public interface IDatabase
    {
        SQLiteAsyncConnection GetOrCreateConnection();
        Task InitializeSQLiteAsync(CancellationToken cancellationToken);
    }
}