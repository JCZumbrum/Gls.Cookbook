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
    public class MeasurementsViewModel : ObservableObject, IViewModel<EmptyArgs>
    {
        private IQueryMeasurementService queryMeasurementService;

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
        }

        public ObservableCollection<ObservableMeasurement> UsMeasurements { get; } = new ObservableCollection<ObservableMeasurement>();
        public ObservableCollection<ObservableMeasurement> MetricMeasurements { get; } = new ObservableCollection<ObservableMeasurement>();

        public MeasurementsViewModel(IQueryMeasurementService queryMeasurementService)
        {
            this.queryMeasurementService = queryMeasurementService;
        }

        public async Task InitializeAsync(EmptyArgs args)
        {
            List<Measurement> allMeasurements = await queryMeasurementService.GetAllAsync();

            var measurementGroupings =  allMeasurements.GroupBy(m => m.MeasurementSystem);
            foreach(var measurementGrouping in measurementGroupings)
            {
                switch(measurementGrouping.Key)
                {
                    case MeasurementSystem.UsCustomary:
                        UsMeasurements.AddRange(measurementGrouping.Select(m => new ObservableMeasurement() { }));
                        break;
                    case MeasurementSystem.Metric:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
