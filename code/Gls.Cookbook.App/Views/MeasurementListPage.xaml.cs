using Gls.Cookbook.ViewSystem.ViewModels;

namespace Gls.Cookbook.App.Views;

public partial class MeasurementListPage : ContentPage
{
	public MeasurementListPage(MeasurementListViewModel viewModel)
	{
		InitializeComponent();

        this.BindingContext = viewModel;
    }
}