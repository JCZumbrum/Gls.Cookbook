using CommunityToolkit.Maui;
using Gls.Cookbook.App.Views;
using Gls.Cookbook.DataAccess;
using Gls.Cookbook.Domain;
using Gls.Cookbook.Domain.Commands.Ingredients;
using Gls.Cookbook.Domain.Commands.Measurements;
using Gls.Cookbook.Domain.Queries;
using Gls.Cookbook.Domain.Repositories;
using Gls.Cookbook.Logic.Ingredients;
using Gls.Cookbook.Logic.Measurements;
using Gls.Cookbook.ViewSystem.ViewModels;

namespace Gls.Cookbook.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<INavigationService, ShellNavigationService>();

            builder.Services.AddSingleton<ICookbookContextFactory, EfCookbookContextFactory>();

            // commands
            builder.Services.AddTransient<ICommandService<CreateMeasurementCommand>, CreateMeasurementService>();
            builder.Services.AddTransient<ICommandService<UpdateMeasurementCommand>, UpdateMeasurementService>();
            builder.Services.AddTransient<ICommandService<DeleteMeasurementCommand>, DeleteMeasurementService>();

            builder.Services.AddTransient<ICommandService<CreateIngredientCommand>, CreateIngredientService>();
            builder.Services.AddTransient<ICommandService<UpdateIngredientCommand>, UpdateIngredientService>();
            builder.Services.AddTransient<ICommandService<DeleteIngredientCommand>, DeleteIngredientService>();

            // queries
            builder.Services.AddTransient<IQueryMeasurementService, QueryMeasurementService>();

            // pages
            builder.Services.AddTransient<CreateRecipePage>();
            builder.Services.AddTransient<IngredientsPage>();
            builder.Services.AddTransient<MeasurementsPage>();
            builder.Services.AddTransient<SearchPage>();
            builder.Services.AddTransient<SettingsPage>();
            builder.Services.AddTransient<MeasurementListPage>();
            builder.Services.AddTransient<EditMeasurementPage>();

            // view models
            builder.Services.AddTransient<CreateRecipeViewModel>();
            builder.Services.AddTransient<IngredientsViewModel>();
            builder.Services.AddTransient<MeasurementsViewModel>();
            builder.Services.AddTransient<SearchViewModel>();
            builder.Services.AddTransient<SettingsViewModel>();
            builder.Services.AddTransient<MeasurementListViewModel>();
            builder.Services.AddTransient<EditMeasurementViewModel>();

            CookbookDbContext.Migrate(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));

            return builder.Build();
        }
    }
}