using AiPrompt.Model.Data;
using AiPrompt.Model.Entity;

namespace AiPrompt.Model.Service;

public interface IConfigService {
    public Task SaveAsync<T>(Config<T> config);
    public Task<Config<T>?> GetAsync<T>(string key) ;
    public Config<T>? Get<T>(string key) ;

}
