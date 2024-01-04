using System.Linq.Expressions;
using AiPrompt.Model.Entity;
using SQLite;

namespace AiPrompt.Model.Data;

public abstract class SqliteDatabase(string databasePath, SQLiteOpenFlags flags)
    : SQLiteAsyncConnection(databasePath, flags) {

    public async Task<List<T>> GetListAsync<T>(Expression<Func<T, bool>> where) where T : BaseEntity, new() {
        return await Table<T>().Where(where).ToListAsync();
    }

    public async Task<T?> GetAsync<T>(Guid id) where T : BaseEntity, new() {
        return await Table<T>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public async Task<T?> GetDefaultAsync<T>() where T : BaseEntity, new() {
        return await Table<T>().FirstOrDefaultAsync();
    }
    public T? GetDefault<T>() where T : BaseEntity, new() {
        return Table<T>().FirstOrDefaultAsync().GetAwaiter().GetResult();
    }
    public async Task SaveAsync<T>(T item) where T : BaseEntity, new() {
        var exists = await GetAsync<T>(item.Id);
        if (exists is not null) {
            await UpdateAsync(item);
        }else {
            await InsertAsync(item);
        }
    }

    public async Task<int> DeleteAsync<T>(T item) where T : BaseEntity, new() {
        return await DeleteAsync(item);
    }
}