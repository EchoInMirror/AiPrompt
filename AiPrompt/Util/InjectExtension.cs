using AiPrompt.I18n;
using AiPrompt.Model.Data;
using AiPrompt.Model.Service;
using AiPrompt.Model.Service.Impl;

namespace AiPrompt.Util;

public static class InjectExtension {
    public static MauiAppBuilder ServiceRegister(this MauiAppBuilder builder) {
        builder.Services.AddTransient<Database>(_ => new Database(Database.DbPath,Database.Flags));
        builder.Services.AddSingleton<I18nService>();
        builder.Services.AddSingleton<ISourceService, SourceService>();
        builder.Services.AddSingleton<IPromptService, PromptService>();
        builder.Services.AddSingleton<IConfigService, ConfigService>();
        builder.Services.AddSingleton<Store>();
        return builder;
    }
}