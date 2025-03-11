using Serilog;

namespace ShopAnalytics.Build.Logger;

public static class AppLoggerConfiguration
{
    public static void ConfigureSerilogLogger(this WebApplication app)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(app.Configuration)
            .CreateLogger();

        app.UseSerilogRequestLogging();
    }
}