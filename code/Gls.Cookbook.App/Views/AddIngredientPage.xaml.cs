using Gls.Cookbook.ViewSystem.ViewModels;

namespace Gls.Cookbook.App.Views;

public partial class AddIngredientPage : ContentPage
{
	public AddIngredientPage(AddIngredientViewModel viewModel)
	{
		InitializeComponent();

        this.BindingContext = viewModel;
    }
}