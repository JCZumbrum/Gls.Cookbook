using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gls.Cookbook.Domain;
using Gls.Cookbook.Domain.Models;
using Gls.Cookbook.Domain.Queries;

namespace Gls.Cookbook.ViewSystem.ViewModels
{
    public class VolumeMeasurementsViewModel : ObservableObject, IViewModel<EmptyArgs>
    {
        public class ObservableMeasurement : ObservableObject
        {
            public int Id { get; set; }

            private string name;
            public string Name
            {
                get
                {
                    return name;
                }
                set
                {
                    if (SetProperty(ref name, value))
                        OnPropertyChanged(nameof(FullName));
                }
            }

            private string abbreviation;
            public string Abbreviation
            {
                get
                {
                    return abbreviation;
                }
                set
                {
                    if (SetProperty(ref abbreviation, value))
                        OnPropertyChanged(nameof(FullName));
                }
            }

            public string FullName
            {
                get
                {
                    return $"{Name} ({Abbreviation})";
                }
            }
        }

        private INavigationService navigationService;
        private IQueryMeasurementService queryMeasurementService;

        public ObservableCollection<ObservableMeasurement> UsMeasurements { get; } = new ObservableCollection<ObservableMeasurement>();
        public ObservableCollection<ObservableMeasurement> MetricMeasurements { get; } = new ObservableCollection<ObservableMeasurement>();

        public IAsyncRelayCommand<ObservableMeasurement> MeasurementSelectedCommand { get; }

        public VolumeMeasurementsViewModel(INavigationService navigationService, IQueryMeasurementService queryMeasurementService)
        {
            this.navigationService = navigationService;
            this.queryMeasurementService = queryMeasurementService;
            this.MeasurementSelectedCommand = new AsyncRelayCommand<ObservableMeasurement>(ViewSelectedMeasurement);
        }

        private async Task ViewSelectedMeasurement(ObservableMeasurement arg)
        {
            await navigationService.GoToAsync<EditMeasurementViewModel, int>(arg.Id);
        }

        public async Task InitializeAsync(EmptyArgs args)
        {
            List<Measurement> volumeMeasurements = await queryMeasurementService.GetByTypeAsync(MeasurementType.Volume);

            var measurementGroupings = volumeMeasurements.GroupBy(m => m.MeasurementSystem);
            foreach (var measurementGrouping in measurementGroupings)
            {
                switch (measurementGrouping.Key)
                {
                    case MeasurementSystem.UsCustomary:
                        UsMeasurements.AddRange(measurementGrouping.OrderBy(m => m.Name).Select(m => new ObservableMeasurement() { Id = m.Id, Name = m.Name, Abbreviation = m.Abbreviation }));
                        break;
                    case MeasurementSystem.Metric:
                        MetricMeasurements.AddRange(measurementGrouping.OrderBy(m => m.Name).Select(m => new ObservableMeasurement() { Id = m.Id, Name = m.Name, Abbreviation = m.Abbreviation }));
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
