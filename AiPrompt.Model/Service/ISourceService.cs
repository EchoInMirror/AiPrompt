
using AiPrompt.Model.Entity;

namespace AiPrompt.Model.Service;

public interface ISourceService{

    public IEnumerable<Prompt> ReadPrompts(string sourcePath, string categoryKey);

    public Task<IEnumerable<Prompt>> ReadPromptsAsync(string sourcePath, string categoryKey);

    public IEnumerable<Prompt> ReadCategories(string sourcePath);

    public Task<IEnumerable<Prompt>> ReadCategoriesAsync(string sourcePath);

    public Task<List<Source>> AllSourceAsync();

    public Task<string?> GetSourcePathAsync();

    public IEnumerable<Prompt> ReadPrefabPrompts(string sourcePath);
    public Task<IEnumerable<Prompt>> ReadPrefabPromptsAsync(string sourcePath);
}
