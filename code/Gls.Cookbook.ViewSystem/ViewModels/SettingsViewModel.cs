using System;
using System.Collections.Generic;
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
        private INavigationService navigationService;

        public IAsyncRelayCommand MeasurementsCommand { get; }
        public IAsyncRelayCommand IngredientsCommand { get; }

        public SettingsViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

            this.MeasurementsCommand = new AsyncRelayCommand(ViewMeasurements);
            this.IngredientsCommand = new AsyncRelayCommand(ViewIngredients);
        }

        private async Task ViewMeasurements()
        {
            await navigationService.GoToAsync<MeasurementTypesViewModel, EmptyArgs>(new EmptyArgs());
        }

        private async Task ViewIngredients()
        {
            await navigationService.GoToAsync<IngredientsViewModel, EmptyArgs>(new EmptyArgs());
        }

        public Task InitializeAsync(EmptyArgs args) { return Task.CompletedTask; }
    }
}
