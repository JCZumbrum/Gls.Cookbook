using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Gls.Cookbook.Domain;
using Gls.Cookbook.Domain.Models;
using Gls.Cookbook.Domain.Queries;

namespace Gls.Cookbook.ViewSystem.ViewModels
{
    public class WeightMeasurementsViewModel : ObservableObject, IViewModel<EmptyArgs>
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
                    SetProperty(ref name, value);
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
                    SetProperty(ref abbreviation, value);
                }
            }
        }

        private IQueryMeasurementService queryMeasurementService;

        public ObservableCollection<ObservableMeasurement> UsMeasurements { get; } = new ObservableCollection<ObservableMeasurement>();
        public ObservableCollection<ObservableMeasurement> MetricMeasurements { get; } = new ObservableCollection<ObservableMeasurement>();

        public WeightMeasurementsViewModel(IQueryMeasurementService queryMeasurementService)
        {
            this.queryMeasurementService = queryMeasurementService;
        }

        public async Task InitializeAsync(EmptyArgs args)
        {
            List<Measurement> volumeMeasurements = await queryMeasurementService.GetByTypeAsync(MeasurementType.Weight);

            var measurementGroupings = volumeMeasurements.GroupBy(m => m.MeasurementSystem);
            foreach (var measurementGrouping in measurementGroupings)
            {
                switch (measurementGrouping.Key)
                {
                    case MeasurementSystem.UsCustomary:
                        UsMeasurements.AddRange(measurementGrouping.Select(m => new ObservableMeasurement() { Id = m.Id, Name = m.Name, Abbreviation = m.Abbreviation }));
                        break;
                    case MeasurementSystem.Metric:
                        MetricMeasurements.AddRange(measurementGrouping.Select(m => new ObservableMeasurement() { Id = m.Id, Name = m.Name, Abbreviation = m.Abbreviation }));
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
