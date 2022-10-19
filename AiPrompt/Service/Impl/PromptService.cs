using AiPrompt.Data;
using AiPrompt.Util;

namespace AiPrompt.Service.Impl;

public class PromptService : IPromptService {

    public PromptService(ISourceService sourceSevice,StateContainer stateContainer){
        this.sourceSevice = sourceSevice;
        this.stateContainer = stateContainer;

    }
    private readonly ISourceService sourceSevice;
    private readonly StateContainer stateContainer;
    public async Task<List<Prompt>> PromptsAsync(string categoryKey)
    {
        var list = await sourceSevice.ReadPromptsAsync(categoryKey);
        return list.Where(a => a.Key.Contains(stateContainer.Query) || a.Name.Contains(stateContainer.Query)).ToList();
    }

    public async Task<List<Prompt>> CategoriesAsync() {
        return (await sourceSevice.ReadCategoriesAsync()).ToList();
    }
}
