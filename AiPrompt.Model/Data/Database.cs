using System.Linq.Expressions;
using AiPrompt.Model.Entity;
using SQLite;

namespace AiPrompt.Model.Data;

public class Database(string databasePath, SQLiteOpenFlags flags) : SQLiteAsyncConnection(databasePath, flags) {
    public static string DbPath =>
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AiPromptSetting.db3");

    public static SQLiteOpenFlags Flags =>
        SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache;
    
    
    public void TableInit<T>(params T[]? dataList) where T: new() {
        var result = CreateTableAsync<T>().GetAwaiter().GetResult();
        if (result != CreateTableResult.Created || dataList is null) return;
        foreach (var data in dataList) {
            InsertAsync(data).GetAwaiter().GetResult();
        }
    }
    
    public async Task<List<T>> GetListAsync<T>(Expression<Func<T, bool>> where) where T : BaseEntity, new() {
        return await Table<T>().Where(where).ToListAsync();
    }

    public async Task<T?> GetAsync<T>(Guid id) where T : BaseEntity, new() {
        return await Table<T>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }
    
    public async Task SaveAsync<T>(T item) where T : BaseEntity, new() {
        var exists = await GetAsync<T>(item.Id);
        if (exists is not null) {
            await UpdateAsync(item);
        }else {
            await InsertAsync(item);
        }
    }

    public async Task<int> RemoveAsync<T>(T item) where T : BaseEntity, new() {
        return await DeleteAsync(item);
    }
}