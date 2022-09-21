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
using Gls.Cookbook.ViewSystem.Args;

namespace Gls.Cookbook.ViewSystem.ViewModels
{
    public class MeasurementListViewModel : ObservableObject, IViewModel<MeasurementType>
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

        private MeasurementType measurementType;

        private string title = "Measurements";
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                SetProperty(ref title, value);
            }
        }

        public ObservableCollection<ObservableMeasurement> UsMeasurements { get; } = new ObservableCollection<ObservableMeasurement>();
        public ObservableCollection<ObservableMeasurement> MetricMeasurements { get; } = new ObservableCollection<ObservableMeasurement>();

        public IAsyncRelayCommand<ObservableMeasurement> MeasurementSelectedCommand { get; }

        public MeasurementListViewModel(INavigationService navigationService, IQueryMeasurementService queryMeasurementService)
        {
            this.navigationService = navigationService;
            this.queryMeasurementService = queryMeasurementService;
            this.MeasurementSelectedCommand = new AsyncRelayCommand<ObservableMeasurement>(ViewSelectedMeasurement);
        }

        private async Task ViewSelectedMeasurement(ObservableMeasurement arg)
        {
            await navigationService.GoToAsync<EditMeasurementViewModel, int>(arg.Id);
        }

        public async Task InitializeAsync(MeasurementType args)
        {
            this.measurementType = args;

            this.Title = $"{measurementType}s";

            List<Measurement> measurements = await queryMeasurementService.GetByTypeAsync(args);

            var measurementGroupings = measurements.GroupBy(m => m.MeasurementSystem);
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
