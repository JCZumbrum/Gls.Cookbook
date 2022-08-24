using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.Domain.Models;

namespace Gls.Cookbook.DataAccess.Models
{
    public static class MeasurementEntityExtensions
    {
        public static MeasurementEntity MapToEntity(this Measurement measurement)
        {
            return new MeasurementEntity()
            {
                Id = measurement.Id,
                MeasurementType = measurement.MeasurementType,
                MeasurementSystem = measurement.MeasurementSystem,
                Name = measurement.Name,
                Abbreviation = measurement.Abbreviation
            };
        }
    }
}
