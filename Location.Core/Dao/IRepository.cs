using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Location.Dao
{
    public interface IRepository<T>
    {
        Task<int> InsertAllAsync(IEnumerable<T> entities);
        Task<int> InsertAsync(T item);
        Task<int> InsertOrReplaceAsync(T item);
        Task<IList<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> FirstOrDefaultAsync();
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<int> DeleteAsync(T item);
        Task<int> DeleteAsync(int id);
        Task<int> DeleteAllAsync();
        Task<List<T>> AllAsync();
        Task<int> UpdateAsync(T item);
        Task<List<T>> AllAsync(int batchSize, int batchNumber);
    }
}