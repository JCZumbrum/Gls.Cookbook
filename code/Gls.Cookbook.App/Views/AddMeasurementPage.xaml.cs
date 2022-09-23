using Gls.Cookbook.ViewSystem.ViewModels;

namespace Gls.Cookbook.App.Views;

public partial class AddMeasurementPage : ContentPage
{
	public AddMeasurementPage(AddMeasurementViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
	}
}