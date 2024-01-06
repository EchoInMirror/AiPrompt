using AiPrompt.Model.Entity;

namespace AiPrompt.Model.Service.Impl;

public class PromptService(ISourceService sourceService) : IPromptService {
    public async Task<List<Prompt>> GetPromptsAsync(string sourcePath, string categoryKey, string? query) {
        var list = await sourceService.ReadPromptsAsync(sourcePath, categoryKey);
        if (query is not null and not "") {
            list = list.Where(a => a.Key.Contains(query) || a.Name.Contains(query));
        }
        return list.ToList();
    }

    public async Task<List<Prompt>> GetCategoriesAsync(string sourcePath) {
        return (await sourceService.ReadCategoriesAsync(sourcePath)).ToList();
    }
}