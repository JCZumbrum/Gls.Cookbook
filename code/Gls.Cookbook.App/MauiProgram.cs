using Gls.Cookbook.DataAccess;
using Gls.Cookbook.Domain;
using Gls.Cookbook.Domain.Commands;
using Gls.Cookbook.Domain.Repositories;

namespace Gls.Cookbook.App
{
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
                });

            builder.Services.AddSingleton<ICookbookContextFactory, EfCookbookContextFactory>();

            return builder.Build();
        }
    }
}