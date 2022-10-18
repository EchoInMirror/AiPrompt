using AiPrompt.Data;
using AiPrompt.Service.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiPrompt.Service;

public interface ISourceService{

    public void SetSource(Source source);
    public IEnumerable<Prompt> ReadPrompts(string categoryKey);

    public Task<IEnumerable<Prompt>> ReadPromptsAsync(string categoryKey);

    public IEnumerable<Prompt> ReadCategories();

    public Task<IEnumerable<Prompt>> ReadCategoriesAsync();

    public Task<IEnumerable<Source>> AllSourceAsync();

    public IEnumerable<Prompt> ReadPrefabPrompts(string key);
    public Task<IEnumerable<Prompt>> ReadPrefabPromptsAsync(string key);
}
