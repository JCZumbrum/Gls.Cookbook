using Gls.Cookbook.DataAccess;
using Gls.Cookbook.Domain;
using Gls.Cookbook.Domain.Commands.Measurements;
using Gls.Cookbook.Domain.Repositories;
using Gls.Cookbook.Logic.Measurements;
using Microsoft.Extensions.DependencyInjection;

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
            builder.Services.AddTransient<ICommandService<CreateMeasurementCommand>, CreateMeasurementService>();
            builder.Services.AddTransient<ICommandService<UpdateMeasurementCommand>, UpdateMeasurementService>();
            builder.Services.AddTransient<ICommandService<DeleteMeasurementCommand>, DeleteMeasurementService>();

            return builder.Build();
        }
    }
}