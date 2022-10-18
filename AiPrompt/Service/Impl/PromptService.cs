using AiPrompt.Data;
using OfficeOpenXml;
using System.Linq;

namespace AiPrompt.Service.Impl;

public class PromptService : IPromptService {

    public PromptService(ISourceService sourceSevice,StateContainer stateContainer){
        this.sourceSevice = sourceSevice;
        this.stateContainer = stateContainer;

    }

    private readonly ISourceService sourceSevice;
    private readonly StateContainer stateContainer;
    /// <summary>
    /// 咒语列表
    /// </summary>
    /// <param name="categoryKey"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Prompt>> ListAsync(string categoryKey) => categoryKey switch {
        "prefab" => await PrefabPromptsAsync(),
        _ => await PromptsAsync(categoryKey)
    };


    public async Task<IEnumerable<Prompt>> PromptsAsync(string categoryKey)
    {
        var list = await sourceSevice.ReadPromptsAsync(categoryKey);
        return list.Where(a => a.Key.Contains(stateContainer.Query) || a.Name.Contains(stateContainer.Query));
    }

    public async Task<IEnumerable<Prompt>> CategoriesAsync() {
        return await sourceSevice.ReadCategoriesAsync();
    }

    public async Task<IEnumerable<Prompt>> PrefabPromptsAsync()
    {
        var list = await sourceSevice.ReadPrefabPromptsAsync(stateContainer.Query);
        return list.Where(a => a.Key.Contains(stateContainer.Query) || a.Name.Contains(stateContainer.Query));
    }
}
