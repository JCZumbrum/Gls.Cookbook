using Gls.Cookbook.App.Views;

namespace Gls.Cookbook.App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MeasurementTypesPage), typeof(MeasurementTypesPage));
            Routing.RegisterRoute(nameof(MeasurementsPage), typeof(MeasurementsPage));
            Routing.RegisterRoute(nameof(AddMeasurementPage), typeof(AddMeasurementPage));
            Routing.RegisterRoute(nameof(EditMeasurementPage), typeof(EditMeasurementPage));

            Routing.RegisterRoute(nameof(IngredientsPage), typeof(IngredientsPage));
            Routing.RegisterRoute(nameof(AddIngredientPage), typeof(AddIngredientPage));
            Routing.RegisterRoute(nameof(EditIngredientPage), typeof(EditIngredientPage));
        }
    }
}