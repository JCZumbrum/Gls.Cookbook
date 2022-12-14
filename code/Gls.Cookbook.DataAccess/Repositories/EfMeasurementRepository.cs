using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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

        public async Task<int> AddAsync(Measurement measurement)
        {
            MeasurementEntity measurementEntity = measurement.MapToEntity();
            await dbContext.Measurements.AddAsync(measurementEntity);
            await dbContext.SaveChangesAsync();

            return measurementEntity.Id;
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
            return await dbContext.Measurements.Select(e => e.MapToDomain()).ToListAsync();
        }

        public async Task<Measurement> GetByIdAsync(int measurementId)
        {
            MeasurementEntity measurementEntity = await dbContext.Measurements.FirstOrDefaultAsync(m => m.Id == measurementId);
            return measurementEntity.MapToDomain();
        }

        public async Task<Measurement> GetByNameTypeAndSystemAsync(string name, MeasurementType measurementType, MeasurementSystem measurementSystem)
        {
            MeasurementEntity measurementEntity = await dbContext.Measurements.FirstOrDefaultAsync(m => m.Name == name && m.MeasurementType == measurementType && m.MeasurementSystem == measurementSystem);
            return measurementEntity.MapToDomain();
        }

        public async Task<List<Measurement>> GetByTypeAsync(MeasurementType measurementType)
        {
            IQueryable<MeasurementEntity> entities = dbContext.Measurements.Where(m => m.MeasurementType == measurementType);
            return await entities.Select(e => e.MapToDomain()).ToListAsync();
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
