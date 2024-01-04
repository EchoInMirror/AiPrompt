using AiPrompt.Model.Entity;
using SQLite;

namespace AiPrompt.Model.Data;

public class Database() : SqliteDatabase(DbPath, Flags) {
    private static string DbPath =>
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AiPromptSetting.db3");

    private static SQLiteOpenFlags Flags =>
        SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache;
    
    
    public void TableInit<T>(params T[]? dataList) where T: BaseEntity, new() {
        var result = CreateTableAsync<T>().GetAwaiter().GetResult();
        if (result == CreateTableResult.Created && dataList is not null) {
            foreach (var data in dataList) {
                InsertAsync(data).GetAwaiter().GetResult();
            }
        }
    }
}