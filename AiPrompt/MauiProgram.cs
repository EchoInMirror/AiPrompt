using AiPrompt.Service;
using AiPrompt.Service.Impl;
using AiPrompt.Util;

namespace AiPrompt;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();
		#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif
		builder.Services.AddMasaBlazor(options =>
		{
			options.UseTheme(themeOptions =>
			{
				themeOptions.Primary = "#7160E8";
			});
		});
        builder.Services.AddSingleton<Db>();
        builder.Services.AddSingleton<ISourceService, SourceService>();
        builder.Services.AddSingleton<IPromptService, PromptService>();
        builder.Services.AddSingleton<IConfigService, ConfigService>();
        builder.Services.AddSingleton<StateContainer>();

        return builder.Build();
	}
}
