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
    public class VolumeMeasurementsViewModel : ObservableObject, IViewModel<EmptyArgs>
    {
        private IQueryMeasurementService queryMeasurementService;

        public ObservableCollection<ObservableMeasurement> UsMeasurements { get; } = new ObservableCollection<ObservableMeasurement>();
        public ObservableCollection<ObservableMeasurement> MetricMeasurements { get; } = new ObservableCollection<ObservableMeasurement>();

        public VolumeMeasurementsViewModel(IQueryMeasurementService queryMeasurementService)
        {
            this.queryMeasurementService = queryMeasurementService;
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
