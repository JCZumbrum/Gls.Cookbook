using CommunityToolkit.Maui;
using Gls.Cookbook.App.Views;
using Gls.Cookbook.DataAccess;
using Gls.Cookbook.Domain;
using Gls.Cookbook.Domain.Commands.Ingredients;
using Gls.Cookbook.Domain.Commands.Measurements;
using Gls.Cookbook.Domain.Models;
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
                    fonts.AddFont("seguisym.ttf", "SegoeUiSymbol");
                });

            builder.Services.AddSingleton<INavigationService, ShellNavigationService>();
            builder.Services.AddSingleton<IToastService, ToastService>();
            builder.Services.AddSingleton<ISnackBarService, SnackBarService>();

            builder.Services.AddSingleton<ICookbookContextFactory, EfCookbookContextFactory>();

            // commands
            builder.Services.AddTransient<ICommandService<CreateMeasurementCommand, Measurement>, CreateMeasurementService>();
            builder.Services.AddTransient<ICommandService<UpdateMeasurementCommand, Measurement>, UpdateMeasurementService>();
            builder.Services.AddTransient<ICommandService<DeleteMeasurementCommand>, DeleteMeasurementService>();

            builder.Services.AddTransient<ICommandService<CreateIngredientCommand, Ingredient>, CreateIngredientService>();
            builder.Services.AddTransient<ICommandService<UpdateIngredientCommand, Ingredient>, UpdateIngredientService>();
            builder.Services.AddTransient<ICommandService<DeleteIngredientCommand>, DeleteIngredientService>();

            // queries
            builder.Services.AddTransient<IQueryMeasurementService, QueryMeasurementService>();
            builder.Services.AddTransient<IQueryIngredientService, QueryIngredientService>();

            // pages
            builder.Services.AddTransient<RecipesPage>();
            builder.Services.AddTransient<CreateRecipePage>();
            builder.Services.AddTransient<IngredientsPage>();
            builder.Services.AddTransient<AddIngredientPage>();
            builder.Services.AddTransient<EditIngredientPage>();
            builder.Services.AddTransient<MeasurementTypesPage>();
            builder.Services.AddTransient<MeasurementsPage>();
            builder.Services.AddTransient<AddMeasurementPage>();
            builder.Services.AddTransient<EditMeasurementPage>();

            // view models
            builder.Services.AddTransient<RecipesViewModel>();
            builder.Services.AddTransient<CreateRecipeViewModel>();
            builder.Services.AddTransient<IngredientsViewModel>();
            builder.Services.AddTransient<AddIngredientViewModel>();
            builder.Services.AddTransient<EditIngredientViewModel>();
            builder.Services.AddTransient<MeasurementTypesViewModel>();
            builder.Services.AddTransient<MeasurementsViewModel>();
            builder.Services.AddTransient<AddMeasurementViewModel>();
            builder.Services.AddTransient<EditMeasurementViewModel>();

            CookbookDbContext.Migrate(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));

            return builder.Build();
        }
    }
}