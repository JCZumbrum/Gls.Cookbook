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
using static Gls.Cookbook.ViewSystem.ViewModels.MeasurementsViewModel;

namespace Gls.Cookbook.ViewSystem.ViewModels
{
    public class MeasurementTypesViewModel : ObservableObject, IViewModel<EmptyArgs>
    {
        public class ObservableMeasurementType : ObservableObject
        {
            public MeasurementType MeasurementType { get; set; }
            public string Name { get; set; }
        }

        private INavigationService navigationService;

        public ObservableCollection<ObservableMeasurementType> MeasurementTypes { get; } = new ObservableCollection<ObservableMeasurementType>();

        public IAsyncRelayCommand<ObservableMeasurementType> MeasurementTypeSelectedCommand { get; }

        public MeasurementTypesViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

            this.MeasurementTypeSelectedCommand = new AsyncRelayCommand<ObservableMeasurementType>(SelectMeasurementType);

            this.MeasurementTypes.Add(new ObservableMeasurementType() { MeasurementType = MeasurementType.Volume, Name = "Volume" });
            this.MeasurementTypes.Add(new ObservableMeasurementType() { MeasurementType = MeasurementType.Weight, Name = "Weight" });
            this.MeasurementTypes.Add(new ObservableMeasurementType() { MeasurementType = MeasurementType.Each, Name = "Eaches" });
        }

        private async Task SelectMeasurementType(ObservableMeasurementType arg)
        {
            await navigationService.GoToAsync<MeasurementsViewModel, MeasurementType>(arg.MeasurementType);
        }

        public Task InitializeAsync(EmptyArgs args) { return Task.CompletedTask; }
    }
}
