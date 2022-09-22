using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gls.Cookbook.Domain;
using Gls.Cookbook.ViewSystem.Args;

namespace Gls.Cookbook.ViewSystem.ViewModels
{
    public class SettingsViewModel : ObservableObject, IViewModel<EmptyArgs>
    {
        public class ObservableSetting : ObservableObject
        {
            public string Name { get; set; }
            public Func<Task> Select { get; set; }
        }

        private INavigationService navigationService;

        public ObservableCollection<ObservableSetting> Settings { get; } = new ObservableCollection<ObservableSetting>();

        public IAsyncRelayCommand<ObservableSetting> SettingSelectedCommand { get; }

        public SettingsViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

            this.SettingSelectedCommand = new AsyncRelayCommand<ObservableSetting>(SelectSetting);

            this.Settings.Add(new ObservableSetting() { Name = "Measurements", Select = async () => await navigationService.GoToAsync<MeasurementTypesViewModel, EmptyArgs>(new EmptyArgs()) });
            this.Settings.Add(new ObservableSetting() { Name = "Ingredients", Select = async () => await navigationService.GoToAsync<IngredientsViewModel, EmptyArgs>(new EmptyArgs()) });
        }

        private async Task SelectSetting(ObservableSetting arg)
        {
            await arg.Select();
        }

        public Task InitializeAsync(EmptyArgs args) { return Task.CompletedTask; }
    }
}
