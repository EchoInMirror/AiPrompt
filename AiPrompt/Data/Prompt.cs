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
        image = LocalFile2Base64(value);
    }}

    private string LocalFile2Base64(string value)
    {
        if (!File.Exists(value)){
            return value;
        }
        string base64 = $"data:image/png;base64,{Convert.ToBase64String(File.ReadAllBytes(value))}";
        return base64;
    }

    public bool Active { get; set; } 


}

