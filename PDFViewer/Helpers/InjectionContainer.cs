using PDFViewer.Interfaces;
using PDFViewer.Services;
using PDFViewer.ViewModels;

namespace PDFViewer.Helpers;

public static class InjectionContainer
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        var i = new ServiceCollection()
            .AddTransient<PDFReaderViewModel>()
            .AddSingleton<IListAssets, ListResAssets>();
            
        services = i;
        return services;
    }
}