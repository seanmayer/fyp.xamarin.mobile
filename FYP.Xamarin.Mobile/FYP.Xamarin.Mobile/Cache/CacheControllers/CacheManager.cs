using FYP.Xamarin.Mobile.Helpers;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FYP.Xamarin.Mobile.Database
{
    public class CacheManager<T> : ICacheManager<T> where T : class, new()
    {
        private SQLiteAsyncConnection db;

        public CacheManager()
        {
            var dbPath = DependencyService.Get<IFileHelper>().GetLocalFilePath("SCP_Databasev1.db3.db3");
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<T>().Wait();
            ClearTables();

        }

        public void ClearTables()
        {
            //db.ExecuteAsync("DELETE FROM Credentials");
            //db.ExecuteAsync("DELETE FROM Athlete");
            //db.ExecuteAsync("DELETE FROM Activity");
            //db.ExecuteAsync("DELETE FROM Power");
            //db.ExecuteAsync("DELETE FROM ActivitySummary");
        }

        public AsyncTableQuery<T> AsQueryable() =>
            db.Table<T>();

        public async Task<List<T>> Get() =>
            await db.Table<T>().ToListAsync();

        public async Task<List<T>> Get<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null)
        {
            var query = db.Table<T>();

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = query.OrderBy<TValue>(orderBy);

            return await query.ToListAsync();
        }

        public async Task<T> Get(int id) =>
             await db.FindAsync<T>(id);

        public async Task<T> Get(Expression<Func<T, bool>> predicate) =>
            await db.FindAsync<T>(predicate);

        public async Task<int> Insert(T entity) =>
             await db.InsertAsync(entity);

        public async Task<int> Update(T entity) =>
             await db.UpdateAsync(entity);

        public async Task<int> Delete(T entity) =>
             await db.DeleteAsync(entity);
    }
}
