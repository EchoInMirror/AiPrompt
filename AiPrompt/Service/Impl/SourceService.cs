using AiPrompt.Data;
using OfficeOpenXml;
using OneOf.Types;

namespace AiPrompt.Service.Impl;

internal class SourceService : ISourceService{

    public SourceService(StateContainer stateContainer){
        this.stateContainer = stateContainer;
    }
    private readonly StateContainer stateContainer;

    /// <summary>
    /// 获取目录下所有文件
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="list"></param>
    /// <returns></returns>
    private async Task FillDirectoryAllSource(string dir, List<Source> list){
        await Task.Run(async () =>{
            DirectoryInfo d = new (dir);
            FileInfo[] files = d.GetFiles();//文件
            DirectoryInfo[] directs = d.GetDirectories();//文件夹
            foreach (FileInfo f in files){
                list.Add(new(f.Name, f.FullName));//添加文件名到列表中  
            }
            //获取子文件夹内的文件列表，递归遍历  
            foreach (DirectoryInfo dd in directs){
                await FillDirectoryAllSource(dd.FullName, list);
            }
        });
    }
    /// <summary>
    /// 获取所有咒语书
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Source>> AllSourceAsync(){
        List<Source> sources = new ();
        var path = Path.Combine(AppContext.BaseDirectory,"Sources");
        await FillDirectoryAllSource(path, sources);
        return sources;
    }
    /// <summary>
    /// 读取咒语
    /// </summary>
    /// <param name="categoryKey">分类键</param>
    /// <returns></returns>
    public IEnumerable<Prompt> ReadPrompts(string categoryKey){
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        List<Prompt> list = new ();
        using (ExcelPackage excelPackage = new (stateContainer.Source?.Path)){
            try{
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets[0];
                for (int i = 1; i <= excelWorksheet.Dimension?.End.Column; i++){
                    if (i % 3 == 1){
                        string category = excelWorksheet.Cells[1, i].GetCellValue<string>();
                        if (category.Equals(categoryKey)){
                            for (int j = 2; j <= excelWorksheet.Dimension?.End.Row; j++){
                                string key = excelWorksheet.Cells[j, i].GetCellValue<string>();
                                if (string.IsNullOrEmpty(key)) break;
                                string name = excelWorksheet.Cells[j, i + 1].GetCellValue<string>();
                                string image = excelWorksheet.Cells[j, i + 2].GetCellValue<string>();
                                list.Add(new () { Name = name, Key = key, Image = image });
                            }
                            break;
                        }
                    }
                }
            }catch{ }
        }
        return list;
    }

    public async Task<IEnumerable<Prompt>> ReadPromptsAsync(string categoryKey){
        return await Task.Run(() => { return ReadPrompts(categoryKey); });
    }


    public async Task<IEnumerable<Prompt>> ReadCategoriesAsync(){
        return await Task.Run(ReadCategories);
    }
    /// <summary>
    /// 获取分类
    /// </summary>
    /// <returns></returns>

    public IEnumerable<Prompt> ReadCategories(){
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        List<Prompt> list = new ();
        using (ExcelPackage excelPackage = new (stateContainer.Source?.Path)){
            try{
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets[0];
                for (int i = 1; i <= excelWorksheet.Dimension?.End.Column; i++){
                    if (i % 3 == 1){
                        string key = excelWorksheet.Cells[1, i].GetCellValue<string>();
                        list.Add(new () { Name = key, Key = key });
                    }
                }
            }catch{ }
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
        List<Prompt> list = new ();
        using (ExcelPackage excelPackage = new (stateContainer.Source?.Path)){
            try {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets[1];
                for (int i = 1; i <= excelWorksheet?.Dimension?.End.Row; i++){
                    string prompts = excelWorksheet.Cells[i, 1].GetCellValue<string>();
                    string name = excelWorksheet.Cells[i, 2].GetCellValue<string>();
                    string image = excelWorksheet.Cells[i, 3].GetCellValue<string>();
                    list.Add(new () { Name = name, Key = prompts, Image = image });
                }
            }catch{ }
        }
        return list;
    }

    public async Task<IEnumerable<Prompt>> ReadPrefabPromptsAsync(string key){
        return await Task.Run(() => { return ReadPrefabPrompts(key); });
    }
}
