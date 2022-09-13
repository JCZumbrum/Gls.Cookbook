using Gls.Cookbook.App.Views;

namespace Gls.Cookbook.App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MeasurementsPage), typeof(MeasurementsPage));
        }
    }
}