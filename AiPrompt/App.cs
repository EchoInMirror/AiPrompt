using AiPrompt.Components;
using AiPrompt.I18n;
using AiPrompt.Model.Constants;
using AiPrompt.Model.Data;
using AiPrompt.Model.Entity;
using AiPrompt.Model.Service;
using AiPrompt.Shared;
using MauiReactor;

namespace AiPrompt;

public sealed class App : BaseComponent{
    public const string Name = "AiPrompt";

    public App() {
        //init database
        var database = Services.GetService<Database>()!;
        CreateTable(database);
        // load config
        var configService = Services.GetService<IConfigService>()!;
        var config = configService.Get<string>(ConfigKeyConstants.Language);
        // load i18n
        var i18NService = Services.GetService<I18nService>()!;
        i18NService.LoadLanguage(config?.Value);
    }

    private static void CreateTable(Database database) {
        database.TableInit(
            new Config<string>(ConfigKeyConstants.Language,Languages.Default),
            new Config<string>(ConfigKeyConstants.SourcePath,string.Empty)
            );
    }

    public override VisualNode Render() => Window(new MainLayout());
}