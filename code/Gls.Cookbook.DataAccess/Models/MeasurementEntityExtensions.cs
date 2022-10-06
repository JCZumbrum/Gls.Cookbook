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
            if (measurement == null)
                return null;

            return new MeasurementEntity()
            {
                Id = measurement.Id,
                MeasurementType = measurement.MeasurementType,
                MeasurementSystem = measurement.MeasurementSystem,
                Name = measurement.Name,
                Abbreviation = measurement.Abbreviation
            };
        }

        public static Measurement MapToDomain(this MeasurementEntity entity)
        {
            if (entity == null)
                return null;

            return new Measurement()
            {
                Id = entity.Id,
                Name = entity.Name,
                Abbreviation = entity.Abbreviation,
                MeasurementType = entity.MeasurementType,
                MeasurementSystem = entity.MeasurementSystem
            };
        }
    }
}
