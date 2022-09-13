using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gls.Cookbook.Domain;

namespace Gls.Cookbook.ViewSystem.ViewModels
{
    public class SettingsViewModel : ObservableObject, IViewModel
    {
        private INavigationService navigationService;

        public IAsyncRelayCommand MeasurementsCommand { get; }

        public SettingsViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

            this.MeasurementsCommand = new AsyncRelayCommand(ViewMeasurements);
        }

        private async Task ViewMeasurements()
        {
            await navigationService.GoToAsync<MeasurementsViewModel>();
        }
    }
}
