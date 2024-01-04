namespace AiPrompt.Model.Entity;

/// <summary>
/// 咒语
/// </summary>
public class Prompt {
    public string Key { get; set; }
    public string Name { get; set; }

    private string? _image;

    public string? Image {
        get => _image;
        init => LocalFile2Base64(value);
    }

    private async void LocalFile2Base64(string? value) {
        if (!File.Exists(value)) {
            return;
        }

        _image = $"data:image/png;base64,{Convert.ToBase64String(await File.ReadAllBytesAsync(value))}";
    }

}