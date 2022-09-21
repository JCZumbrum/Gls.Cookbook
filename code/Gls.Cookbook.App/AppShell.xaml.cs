using Gls.Cookbook.App.Views;

namespace Gls.Cookbook.App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(IngredientsPage), typeof(IngredientsPage));
            Routing.RegisterRoute(nameof(MeasurementsPage), typeof(MeasurementsPage));
            Routing.RegisterRoute(nameof(VolumeMeasurementsPage), typeof(VolumeMeasurementsPage));
            Routing.RegisterRoute(nameof(WeightMeasurementsPage), typeof(WeightMeasurementsPage));
            Routing.RegisterRoute(nameof(EditMeasurementPage), typeof(EditMeasurementPage));            
        }
    }
}