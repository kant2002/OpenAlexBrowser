using Microsoft.Extensions.DependencyInjection;
using OpenAlexNet;

namespace OpenAlexBrowser.App;

public static class OpenAlexBrowserExtensions
{
    public static void AddOpenAlexBrowser(this IServiceCollection services)
    {
        services.AddScoped<OpenAlexApi>();
        services.AddScoped<FilesInterop>();
    }
}
