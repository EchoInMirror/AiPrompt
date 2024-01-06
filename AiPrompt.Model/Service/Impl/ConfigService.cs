using AiPrompt.Model.Data;
using AiPrompt.Model.Entity;

namespace AiPrompt.Model.Service.Impl;

public class ConfigService(Database database) : IConfigService{

    public async Task SaveAsync<T>(Config<T> config) {
        var exists = await GetAsync<T>(config.Key);
        var cache = config.ToStr();
        if (exists != null) {
            await database.UpdateAsync(cache);
        }else {
            await database.InsertAsync(cache);
        }
    }

    public async Task<Config<T>?> GetAsync<T>(string key) {
        var get = await database
            .Table<Config<string>>().Where(i => i.Key == key)
            .FirstOrDefaultAsync();
        var config = get?.To<T>();
        return config;
    }
    
    public Config<T>? Get<T>(string key) {
        var get = database
            .Table<Config<string>>().Where(i => i.Key == key)
            .FirstOrDefaultAsync().GetAwaiter().GetResult();
        var cache = get?.To<T>();
        return cache;
    }
}
