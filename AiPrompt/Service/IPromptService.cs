using AiPrompt.Data;

namespace AiPrompt.Service;

public interface IPromptService{
    public Task<IEnumerable<Prompt>> ListAsync(string categroyKey);
    public Task<IEnumerable<Prompt>> CategoriesAsync();
    public Task<IEnumerable<Prompt>> PrefabPromptsAsync();
    public Task<IEnumerable<Prompt>> PromptsAsync(string categroyKey);

}
