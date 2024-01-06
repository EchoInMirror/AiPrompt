using AiPrompt.Model.Entity;

namespace AiPrompt.Model.Service;

public interface IPromptService{
    public Task<List<Prompt>> GetCategoriesAsync(string sourcePath);
    public Task<List<Prompt>> GetPromptsAsync(string sourcePath,string categoryKey,string? query);

}
