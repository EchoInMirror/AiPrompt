using AiPrompt.Model.Entity;

namespace AiPrompt.Model.Service;

public interface IPromptService{
    public Task<List<Prompt>> GetCategoriesAsync(string filepath);
    public Task<List<Prompt>> GetPromptsAsync(string categoryKey);

}
