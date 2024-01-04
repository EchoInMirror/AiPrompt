
using AiPrompt.Model.Entity;

namespace AiPrompt.Model.Service;

public interface ISourceService{

    public void SetSource(Source source);
    public IEnumerable<Prompt> ReadPrompts(string categoryKey);

    public Task<IEnumerable<Prompt>> ReadPromptsAsync(string categoryKey);

    public IEnumerable<Prompt> ReadCategories(string filepath);

    public Task<IEnumerable<Prompt>> ReadCategoriesAsync(string filepath);

    public Task<List<Source>> AllSourceAsync();

    public Task<string?> GetSourcePathAsync();

    public IEnumerable<Prompt> ReadPrefabPrompts(string key);
    public Task<IEnumerable<Prompt>> ReadPrefabPromptsAsync(string key);
}
