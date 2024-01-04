using AiPrompt.Components;
using AiPrompt.I18n;
using AiPrompt.Model.Data;
using AiPrompt.Model.Entity;
using AiPrompt.Model.Service;
using AiPrompt.Shared;
using MauiReactor;

namespace AiPrompt;

public class App : BaseComponent{
    public const string Name = "AiPrompt";
    
    public App() {
        //init database
        var database = Services.GetService<Database>()!;
        CreateTable(database);
        // load config
        var configService = Services.GetService<IConfigService>()!;
        var config = configService.Get();
        // load i18n
        var i18NService = Services.GetService<I18nService>()!;
        i18NService.LoadLanguage(config.Language);
    }

    private void CreateTable(Database database) {
        database.TableInit<Config>(new Config {
           Id = Guid.NewGuid(),
           Language = Languages.Default
       });
    }

    public override VisualNode Render() => Window(new MainLayout());
}