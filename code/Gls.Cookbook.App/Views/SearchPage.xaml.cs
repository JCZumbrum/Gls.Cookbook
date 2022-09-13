using Gls.Cookbook.ViewSystem.ViewModels;

namespace Gls.Cookbook.App.Views;

public partial class SearchPage : ContentPage
{
	public SearchPage(SearchViewModel searchViewModel)
	{
		InitializeComponent();

		this.BindingContext = searchViewModel;
	}
}