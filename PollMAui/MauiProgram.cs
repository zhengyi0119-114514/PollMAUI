using Microsoft.Extensions.Logging;

namespace PollMAui;

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
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("CEFFontsCJKMono-Regular.ttf", "CEF Fonts CJK");
				fonts.AddFont("CEFFontsCJKMono-Regular.ttf", "CEF Fonts CJK Mono");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
