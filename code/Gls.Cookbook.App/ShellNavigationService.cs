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

        public async Task GoToAsync<T>() where T : IViewModel
        {
            Type pageType = viewModelToPageMap[typeof(T)];

            await Shell.Current.GoToAsync(pageType.Name);
        }

        public async Task GoToAsync<T>(IDictionary<string, object> parameters) where T : IViewModel
        {
            Type pageType = viewModelToPageMap[typeof(T)];

            await Shell.Current.GoToAsync(pageType.Name, parameters);
        }
    }
}
