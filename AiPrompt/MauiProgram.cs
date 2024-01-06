using AiPrompt.I18n;
using AiPrompt.Model.Entity;
using AiPrompt.Model.Service;
using AiPrompt.Model.Utils;
using AiPrompt.Util;
using MauiReactor;
using Microsoft.Maui.LifecycleEvents;

namespace AiPrompt;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiReactorApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			})
#if DEBUG
			.EnableMauiReactorHotReload()
#endif
		.ServiceRegister();
		var app = builder.Build();
		Ioc.SetProvider(app.Services);
        return app;
	}
}
