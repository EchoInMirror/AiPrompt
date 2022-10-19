using AiPrompt.Data;

namespace AiPrompt.Service;

public interface IPromptService{
    public Task<List<Prompt>> CategoriesAsync();
    public Task<List<Prompt>> PromptsAsync(string categoryKey);

}
