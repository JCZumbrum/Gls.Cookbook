using Gls.Cookbook.ViewSystem.ViewModels;

namespace Gls.Cookbook.App.Views;

public partial class RecipesPage : ContentPage
{
	public RecipesPage(RecipesViewModel viewModel)
	{
		InitializeComponent();

        this.BindingContext = viewModel;
    }
}