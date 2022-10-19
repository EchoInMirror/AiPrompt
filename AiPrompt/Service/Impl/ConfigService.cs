using AiPrompt.Data;
using AiPrompt.Util;

namespace AiPrompt.Service.Impl;

public class ConfigService : IConfigService{
    public ConfigService(Db db)
    {
        this.db = db;
    }
    private readonly Db db;

    // private string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");


    public async Task SaveConfig(Config config)
    {
        config.Id = nameof(config);
        await db.SaveAsync(config);
        //using(var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
        //{
        //    try
        //    {
        //        await JsonSerializer.SerializeAsync<Config>(fs, config,new JsonSerializerOptions(){ WriteIndented = true });
        //    }
        //    catch { }
        //}
    }

    public async Task<Config> GetConfig()
    {
        return await db.GetAsync<Config>();
        //using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
        //{
        //    try
        //    {
        //        var config = await JsonSerializer.DeserializeAsync<Config>(fs);
        //        return config;
        //    }
        //    catch { return null; }

        //}
    }
}
