using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Location.Models;

namespace Location.Dao
{
    public class Repository<T> : IRepository<T> where T : class, IEntity, new()
    {
        #region Declarations

        private readonly IDatabase _context;

        #endregion

        #region Properties

        protected IDatabase Context
        {
            get { return _context; }
        }

        #endregion

        #region Constructors

        public Repository(IDatabase context)
        {
            this._context = context;
        }

        #endregion

        #region Public Methods

        public async Task<int> InsertAllAsync(IEnumerable<T> items)
        {
            return await this._context.GetOrCreateConnection().InsertAllAsync(items);
        }

        public async Task<int> InsertAsync(T item)
        {
            return await this._context.GetOrCreateConnection().InsertAsync(item);
        }

        public async Task<int> InsertOrReplaceAsync(T item)
        {
            return await this._context.GetOrCreateConnection().InsertOrReplaceAsync(item);
        }

        public async Task<IList<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await this._context.GetOrCreateConnection().Table<T>().Where(predicate).ToListAsync();
        }

        public async Task<int> UpdateAsync(T item)
        {
            return await this._context.GetOrCreateConnection().UpdateAsync(item);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await this._context.GetOrCreateConnection().Table<T>().Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<T> FirstOrDefaultAsync()
        {
            return await this._context.GetOrCreateConnection().Table<T>().FirstOrDefaultAsync();
        }

        public async Task<int> DeleteAsync(T item)
        {
            return await this._context.GetOrCreateConnection().DeleteAsync(item);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await this._context.GetOrCreateConnection().DeleteAsync(id);
        }

        public async Task<int> DeleteAllAsync()
        {
            return await this._context.GetOrCreateConnection().DeleteAllAsync<T>();
        }

        public async Task<List<T>> AllAsync()
        {
            return await this._context.GetOrCreateConnection().Table<T>().ToListAsync();
        }

        public async Task<List<T>> AllAsync(int batchSize, int batchNumber)
        {
            var elementToSkip = batchSize * batchNumber;

            return await this._context.GetOrCreateConnection().Table<T>().Skip(elementToSkip).Take(batchSize).ToListAsync();

        }

        #endregion

    }
}