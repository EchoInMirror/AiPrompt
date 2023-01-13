using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiPrompt.Data;
/// <summary>
/// 咒语
/// </summary>
public class Prompt{
    public string Key { get; set; }
    public string Name { get; set; }

    private string image;
    public string Image { get=> image; set{
        LocalFile2Base64(value);
    }}

    private async void LocalFile2Base64(string value)
    {
        if (!File.Exists(value)){
            return ;
        }
        image = $"data:image/png;base64,{Convert.ToBase64String(await File.ReadAllBytesAsync(value))}";
    }

    public bool Active { get; set; } 


}

