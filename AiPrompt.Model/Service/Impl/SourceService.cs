using AiPrompt.Model.Entity;
using AiPrompt.Util;
using OfficeOpenXml;

namespace AiPrompt.Model.Service.Impl;

public class SourceService(StateContainer stateContainer, IConfigService configService) : ISourceService {
    /// <summary>
    /// 获取目录下所有文件
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="list"></param>
    /// <returns></returns>
    private async Task FillDirectoryAllSource(string? dir, List<Source> list){
        await Task.Run(async () =>{
            if (dir != null) {
                var d = new DirectoryInfo(dir);
                if (d.Exists) {
                    var files = d.GetFiles();//文件
                    var directs = d.GetDirectories();//文件夹
                    foreach (var f in files) {
                        if (f.Extension is ".xlsx" or ".xls") {
                            list.Add(new Source(f.Name, f.FullName));//添加文件名到列表中  
                        }
                    }
                    //获取子文件夹内的文件列表，递归遍历  
                    foreach (var dd in directs) {
                        await FillDirectoryAllSource(dd.FullName, list);
                    }
                }
            }
        });
    }
    /// <summary>
    /// 获取所有咒语书
    /// </summary>
    /// <returns></returns>
    public async Task<List<Source>> AllSourceAsync(){
        List<Source> sources = [];
        var path = await GetSourcePathAsync();
        await FillDirectoryAllSource(path, sources);
        return sources;
    }

    public async Task<string?> GetSourcePathAsync(){
        Config config = await configService.GetAsync();
        return config.SourcePath;
    }
    /// <summary>
    /// 读取咒语
    /// </summary>
    /// <param name="categoryKey">分类键</param>
    /// <returns></returns>
    public IEnumerable<Prompt> ReadPrompts(string categoryKey){
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        List<Prompt> list = [];
        using ExcelPackage excelPackage = new(stateContainer.Source.Path);
        var excelWorksheet = excelPackage.Workbook.Worksheets[0];
        for (var i = 1; i <= excelWorksheet.Dimension?.End.Column; i++) {
            if (i % 3 != 1) continue;
            var category = excelWorksheet.Cells[1, i].GetCellValue<string>();
            if (!category.Equals(categoryKey)) continue;
            for (var j = 2; j <= excelWorksheet.Dimension?.End.Row; j++)
            {
                var key = excelWorksheet.Cells[j, i].GetCellValue<string>();
                if (string.IsNullOrEmpty(key)) break;
                var name = excelWorksheet.Cells[j, i + 1].GetCellValue<string>();
                var image = excelWorksheet.Cells[j, i + 2].GetCellValue<string>();
                list.Add(new Prompt { Name = name, Key = key, Image = image });
            }
            break;
        }

        return list;
    }

    public async Task<IEnumerable<Prompt>> ReadPromptsAsync(string categoryKey){
        return await Task.Run(() => ReadPrompts(categoryKey));
    }




    public async Task<IEnumerable<Prompt>> ReadCategoriesAsync(string filepath){
        return await Task.Run(()=>ReadCategories(filepath));
    }
    /// <summary>
    /// 获取分类
    /// </summary>
    /// <returns></returns>

    public IEnumerable<Prompt> ReadCategories(string filepath){
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        List<Prompt> list = [];
        using ExcelPackage excelPackage = new(filepath);
        var excelWorksheet = excelPackage.Workbook.Worksheets[0];
        for (var i = 1; i <= excelWorksheet.Dimension?.End.Column; i++) {
            if (i % 3 != 1) continue;
            var key = excelWorksheet.Cells[1, i].GetCellValue<string>();
            list.Add(new Prompt { Name = key, Key = key });
        }

        return list;
    }

    public void SetSource(Source source){
        stateContainer.Source = source ?? throw new ArgumentNullException();
    }
    /// <summary>
    /// 读取预制咒语
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public IEnumerable<Prompt> ReadPrefabPrompts(string key){
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        List<Prompt> list = [];
        using ExcelPackage excelPackage = new(stateContainer.Source.Path);
        var excelWorksheet = excelPackage.Workbook.Worksheets[1];
        for (var i = 1; i <= excelWorksheet.Dimension?.End.Row; i++)
        {
            var prompts = excelWorksheet.Cells[i, 1].GetCellValue<string>();
            var name = excelWorksheet.Cells[i, 2].GetCellValue<string>();
            var image = excelWorksheet.Cells[i, 3].GetCellValue<string>();
            list.Add(new Prompt { Name = name, Key = prompts, Image = image });
        }
        return list;
    }

    public async Task<IEnumerable<Prompt>> ReadPrefabPromptsAsync(string key){
        return await Task.Run(() => ReadPrefabPrompts(key));
    }


}
