namespace ShopAnalytics.Build.RequestPipeline;

public static class WebApplicationExtensions
{
    public static IApplicationBuilder UseGlobalErrorHandling(this WebApplication app)
    {
        app.UseExceptionHandler("/error");
        return app;
    }
}