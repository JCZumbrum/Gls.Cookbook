using Gls.Cookbook.ViewSystem.ViewModels;

namespace Gls.Cookbook.App.Views;

public partial class VolumeMeasurementsPage : ContentPage
{
	public VolumeMeasurementsPage(VolumeMeasurementsViewModel viewModel)
	{
		InitializeComponent();

        this.BindingContext = viewModel;
    }
}