using Gls.Cookbook.ViewSystem.ViewModels;

namespace Gls.Cookbook.App.Views;

public partial class MeasurementTypesPage : ContentPage
{
	public MeasurementTypesPage(MeasurementTypesViewModel viewModel)
	{
		InitializeComponent();

        this.BindingContext = viewModel;
    }
}