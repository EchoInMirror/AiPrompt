using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using AiPrompt.Util;

namespace AiPrompt.I18n;

public sealed class I18nService {
    private static JsonNode? _words;
    public string? Language { get; private set; }


    public event Action<string>? LanguageChanged;
    
    public void LoadLanguage(string? language) {
        Language = language ?? Languages.Default;
        var json = AssetLoader.LoadString($"{Language}.json");
        _words = JsonSerializer.Deserialize<JsonNode>(json);
        LanguageChanged?.Invoke(Language);
    }

    public string this[string key] => _words?[key]?.ToString()??$"{Language}:{key}";
}