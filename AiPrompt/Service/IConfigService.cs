using AiPrompt.Data;

namespace AiPrompt.Service;

public interface IConfigService
{
    public  Task SaveConfig(Config config);
    public Task<Config> GetConfig();

}
