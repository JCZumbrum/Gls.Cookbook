using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.App.Views;
using Gls.Cookbook.Domain;
using Gls.Cookbook.ViewSystem.ViewModels;

namespace Gls.Cookbook.App
{
    public class ShellNavigationService : INavigationService
    {
        private Dictionary<Type, Type> viewModelToPageMap = new Dictionary<Type, Type>()
        {
            { typeof(SettingsViewModel), typeof(SettingsPage) },
            { typeof(MeasurementsViewModel), typeof(MeasurementsPage) }
        };

        public async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }

        public async Task GoToAsync<TViewModel, TArgs>(TArgs args) where TViewModel : IViewModel<TArgs>
        {
            Type pageType = viewModelToPageMap[typeof(TViewModel)];

            await Shell.Current.GoToAsync(pageType.Name);

            if (Shell.Current.CurrentPage.BindingContext is not TViewModel viewModel)
                throw new InvalidOperationException();

            await viewModel.InitializeAsync(args);
        }
    }
}
