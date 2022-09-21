using Gls.Cookbook.ViewSystem.ViewModels;

namespace Gls.Cookbook.App.Views;

public partial class EditMeasurementPage : ContentPage
{
	public EditMeasurementPage(EditMeasurementViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
	}
}