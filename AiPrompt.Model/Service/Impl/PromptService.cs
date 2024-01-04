using AiPrompt.Model.Entity;
using AiPrompt.Util;

namespace AiPrompt.Model.Service.Impl;

public class PromptService(ISourceService sourceService, StateContainer stateContainer) : IPromptService {
    public async Task<List<Prompt>> GetPromptsAsync(string categoryKey)
    {
        var list = await sourceService.ReadPromptsAsync(categoryKey);
        return list.Where(a => a.Key.Contains(stateContainer.Query) || a.Name.Contains(stateContainer.Query)).ToList();
    }

    public async Task<List<Prompt>> GetCategoriesAsync(string filepath) {
        return (await sourceService.ReadCategoriesAsync(filepath)).ToList();
    }
}
