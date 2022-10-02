using Gls.Cookbook.ViewSystem.ViewModels;

namespace Gls.Cookbook.App.Views;

public partial class EditIngredientPage : ContentPage
{
	public EditIngredientPage(EditIngredientViewModel viewModel)
    {
        InitializeComponent();

        this.BindingContext = viewModel;
    }
}