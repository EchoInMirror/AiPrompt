using AiPrompt.Data;
using SQLite;

namespace AiPrompt.Util;

public class Db
{
    private SQLiteAsyncConnection Database;
    async Task Init()
    {
        if (Database is not null)
            return;

        Database = new SQLiteAsyncConnection(DbConstants.DatabasePath, DbConstants.Flags);
        var result = await Database.CreateTableAsync<Config>();
    }
   
    public async Task<T> GetAsync<T>() where T : BaseModel, new()
    {
        await Init();
        return await Database.Table<T>().FirstOrDefaultAsync();
    }

    public async Task<int> SaveAsync<T>(T t) where T : BaseModel,new()
    {
        await Init();
        var exsist = await Database.Table<T>().FirstOrDefaultAsync();
        if (string.Equals(t?.Id,exsist?.Id))
            return await Database.UpdateAsync(t);
        else
            return await Database.InsertAsync(t);
    }
}
