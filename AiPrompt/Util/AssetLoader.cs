using Microsoft.Maui.Storage;

namespace AiPrompt.Util;

public class AssetLoader {
    public static async Task<Stream> LoadStreamAsync(string filename) { 
        var stream = await FileSystem.OpenAppPackageFileAsync(filename);
        return stream;
    }
    
    public static async Task<string> LoadStringAsync(string filename) {
        var stream = await LoadStreamAsync(filename);
        var streamReader = new StreamReader(stream);
        var @string = await streamReader.ReadToEndAsync();
        return @string;
    }
    
    public static string LoadString(string filename) {
        var stream = FileSystem.OpenAppPackageFileAsync(filename).GetAwaiter().GetResult();
        var streamReader = new StreamReader(stream);
        var @string = streamReader.ReadToEnd();
        return @string;
    }
}