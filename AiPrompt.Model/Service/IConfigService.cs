using AiPrompt.Model.Data;
using AiPrompt.Model.Entity;

namespace AiPrompt.Model.Service;

public interface IConfigService {
    public Task SaveAsync(Config config);
    public Task<Config> GetAsync();
    public Config Get();

}
