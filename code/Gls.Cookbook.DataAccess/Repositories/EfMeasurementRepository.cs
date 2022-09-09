using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.DataAccess.Models;
using Gls.Cookbook.Domain;
using Gls.Cookbook.Domain.Models;
using Gls.Cookbook.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gls.Cookbook.DataAccess.Repositories
{
    public class EfMeasurementRepository : IMeasurementRepository
    {
        private CookbookDbContext dbContext;

        public EfMeasurementRepository(CookbookDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(Measurement measurement)
        {
            MeasurementEntity measurementEntity = measurement.MapToEntity();
            await dbContext.Measurements.AddAsync(measurementEntity);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int measurementId)
        {
            MeasurementEntity measurementEntity = await dbContext.Measurements.FirstOrDefaultAsync(m => m.Id == measurementId);
            if (measurementEntity == null)
                return;

            dbContext.Measurements.Remove(measurementEntity);

            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Measurement>> GetAllAsync()
        {
            return await dbContext.Measurements.Select(e => e.MapToMeasurement()).ToListAsync();
        }

        public async Task<Measurement> GetByIdAsync(int measurementId)
        {
            MeasurementEntity measurementEntity = await dbContext.Measurements.FirstOrDefaultAsync(m => m.Id == measurementId);
            return measurementEntity.MapToMeasurement();
        }

        public async Task<Measurement> GetByNameTypeAndSystemAsync(string name, MeasurementType measurementType, MeasurementSystem measurementSystem)
        {
            MeasurementEntity measurementEntity = await dbContext.Measurements.FirstOrDefaultAsync(m => m.Name == name && m.MeasurementType == measurementType && m.MeasurementSystem == measurementSystem);
            return measurementEntity.MapToMeasurement();
        }

        public async Task UpdateAsync(Measurement measurement)
        {
            MeasurementEntity measurementEntity = await dbContext.Measurements.FirstOrDefaultAsync(m => m.Id == measurement.Id);

            measurementEntity.Name = measurement.Name;
            measurementEntity.Abbreviation = measurement.Abbreviation;
            measurementEntity.MeasurementType = measurement.MeasurementType;
            measurementEntity.MeasurementSystem = measurement.MeasurementSystem;

            dbContext.Measurements.Update(measurementEntity);
            await dbContext.SaveChangesAsync();
        }
    }
}
