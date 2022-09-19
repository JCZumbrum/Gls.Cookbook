using Gls.Cookbook.ViewSystem.ViewModels;

namespace Gls.Cookbook.App.Views;

public partial class IngredientsPage : ContentPage
{
	public IngredientsPage(IngredientsViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
	}
}