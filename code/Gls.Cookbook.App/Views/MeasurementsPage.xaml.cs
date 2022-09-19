using Gls.Cookbook.ViewSystem.ViewModels;

namespace Gls.Cookbook.App.Views;

public partial class MeasurementsPage : ContentPage
{
	public MeasurementsPage(MeasurementsViewModel viewModel)
	{
		InitializeComponent();

        this.BindingContext = viewModel;
    }
}