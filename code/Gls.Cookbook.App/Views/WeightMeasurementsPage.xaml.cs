using Gls.Cookbook.ViewSystem.ViewModels;

namespace Gls.Cookbook.App.Views;

public partial class WeightMeasurementsPage : ContentPage
{
	public WeightMeasurementsPage(WeightMeasurementsViewModel viewModel)
	{
		InitializeComponent();

        this.BindingContext = viewModel;
    }
}