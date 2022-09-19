using Gls.Cookbook.ViewSystem.ViewModels;

namespace Gls.Cookbook.App.Views;

public partial class CreateRecipePage : ContentPage
{
	public CreateRecipePage(CreateRecipeViewModel viewModel)
	{
		InitializeComponent();

        this.BindingContext = viewModel;
    }
}