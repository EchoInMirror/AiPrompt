using AiPrompt.Model.Data;
using AiPrompt.Model.Entity;

namespace AiPrompt.Model.Service.Impl;

public class ConfigService(Database database) : IConfigService{

    /// <summary>
    /// 确保config唯一性
    /// </summary>
    /// <param name="config"></param>
    public async Task SaveAsync(Config config) {
        var exists = await GetAsync();
        var save = config with { Id = exists.Id };
        await database.UpdateAsync(save);
    }

    public async Task<Config> GetAsync() {
        return (await database.GetDefaultAsync<Config>())!;
    }
    
    public Config Get() {
        return database.GetDefault<Config>()!;
    }
}
