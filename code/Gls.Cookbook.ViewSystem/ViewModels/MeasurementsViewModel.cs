using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Gls.Cookbook.Domain;
using Gls.Cookbook.Domain.Models;
using Gls.Cookbook.Domain.Queries;
using Gls.Cookbook.ViewSystem.Messages;

namespace Gls.Cookbook.ViewSystem.ViewModels
{
    public class MeasurementsViewModel : 
        ObservableRecipient,
        IViewModel<MeasurementType>,
        IRecipient<MeasurementAddedMessage>,
        IRecipient<MeasurementUpdatedMessage>,
        IRecipient<MeasurementDeletedMessage>
    {
        public class ObservableMeasurementTypeCollection : ObservableCollection<ObservableMeasurement>
        {
            public string Name { get; set; }
        }

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

        public ObservableCollection<ObservableMeasurementTypeCollection> Measurements { get; } = new ObservableCollection<ObservableMeasurementTypeCollection>();

        private ObservableMeasurementTypeCollection UsMeasurements { get; } = new ObservableMeasurementTypeCollection() { Name = "US" };
        private ObservableMeasurementTypeCollection MetricMeasurements { get; } = new ObservableMeasurementTypeCollection() { Name = "Metric" };
        private ObservableMeasurementTypeCollection UniversalMeasurements { get; } = new ObservableMeasurementTypeCollection() { Name = "Universal" };

        public IAsyncRelayCommand<ObservableMeasurement> MeasurementSelectedCommand { get; }
        public IAsyncRelayCommand AddMeasurementCommand { get; }
        public IRelayCommand LoadedCommand { get; }
        public IRelayCommand UnloadedCommand { get; }

        public MeasurementsViewModel(INavigationService navigationService, IQueryMeasurementService queryMeasurementService)
        {
            this.navigationService = navigationService;
            this.queryMeasurementService = queryMeasurementService;

            this.MeasurementSelectedCommand = new AsyncRelayCommand<ObservableMeasurement>(ViewMeasurement);
            this.AddMeasurementCommand = new AsyncRelayCommand(AddMeasurement);
            this.LoadedCommand = new RelayCommand(Load);
            this.UnloadedCommand = new RelayCommand(Unload);
        }

        private void Load()
        {
            this.IsActive = true;
        }

        private void Unload()
        {
            this.IsActive = false;
        }

        private async Task ViewMeasurement(ObservableMeasurement arg)
        {
            await navigationService.GoToAsync<EditMeasurementViewModel, int>(arg.Id);
        }

        private async Task AddMeasurement()
        {
            await navigationService.GoToAsync<AddMeasurementViewModel, MeasurementType>(measurementType);
        }

        public async Task InitializeAsync(MeasurementType args)
        {
            this.measurementType = args;

            this.Title = measurementType == MeasurementType.Each ? "Eaches" : $"{measurementType}s";

            switch (this.measurementType)
            {
                case MeasurementType.Volume:
                case MeasurementType.Weight:
                    this.Measurements.Add(UsMeasurements);
                    this.Measurements.Add(MetricMeasurements);
                    break;
                case MeasurementType.Each:
                    this.Measurements.Add(UniversalMeasurements);
                    break;
            }

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
                    case MeasurementSystem.Universal:
                        UniversalMeasurements.AddRange(measurementGrouping.OrderBy(m => m.Name).Select(m => new ObservableMeasurement() { Id = m.Id, Name = m.Name, Abbreviation = m.Abbreviation }));
                        break;
                }
            }
        }

        public void Receive(MeasurementAddedMessage message)
        {
            Measurement measurement = message.Measurement;

            if (measurement.MeasurementType != measurementType)
                return;

            switch (measurement.MeasurementSystem)
            {
                case MeasurementSystem.UsCustomary:
                    UsMeasurements.Add(new ObservableMeasurement() { Id = measurement.Id, Name = measurement.Name, Abbreviation = measurement.Abbreviation });
                    break;
                case MeasurementSystem.Metric:
                    MetricMeasurements.Add(new ObservableMeasurement() { Id = measurement.Id, Name = measurement.Name, Abbreviation = measurement.Abbreviation });
                    break;
                case MeasurementSystem.Universal:
                    UniversalMeasurements.Add(new ObservableMeasurement() { Id = measurement.Id, Name = measurement.Name, Abbreviation = measurement.Abbreviation });
                    break;
            }
        }

        public void Receive(MeasurementUpdatedMessage message)
        {
            Measurement measurement = message.Measurement;

            if (measurement.MeasurementType != measurementType)
                return;

            switch (measurement.MeasurementSystem)
            {
                case MeasurementSystem.UsCustomary:
                    ObservableMeasurement usMeasurement = UsMeasurements.FirstOrDefault(m => m.Id == measurement.Id);
                    if (usMeasurement != null)
                    {
                        usMeasurement.Name = measurement.Name;
                        usMeasurement.Abbreviation = measurement.Abbreviation;
                    }
                    break;
                case MeasurementSystem.Metric:
                    ObservableMeasurement metricMeasurement = MetricMeasurements.FirstOrDefault(m => m.Id == measurement.Id);
                    if (metricMeasurement != null)
                    {
                        metricMeasurement.Name = measurement.Name;
                        metricMeasurement.Abbreviation = measurement.Abbreviation;
                    }
                    break;
            }
        }

        public void Receive(MeasurementDeletedMessage message)
        {
            if (message.MeasurementType != measurementType)
                return;

            switch (message.MeasurementSystem)
            {
                case MeasurementSystem.UsCustomary:
                    ObservableMeasurement usMeasurement = UsMeasurements.FirstOrDefault(m => m.Id == message.MeasurementId);
                    if (usMeasurement != null)
                        UsMeasurements.Remove(usMeasurement);
                    break;
                case MeasurementSystem.Metric:
                    ObservableMeasurement metricMeasurement = MetricMeasurements.FirstOrDefault(m => m.Id == message.MeasurementId);
                    if (metricMeasurement != null)
                        MetricMeasurements.Remove(metricMeasurement);
                    break;
            }
        }
    }
}
