namespace AiPrompt.Model.Entity;

/// <summary>
/// 咒语书源
/// </summary>
public class Source(string name, string path) {
    public string Name { get; set; } = name;
    public string Path { get; set; } = path;
}