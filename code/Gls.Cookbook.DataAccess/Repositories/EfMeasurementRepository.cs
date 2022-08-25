using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.Domain.Models;
using Gls.Cookbook.Domain.Repositories;

namespace Gls.Cookbook.DataAccess.Repositories
{
    public class EfMeasurementRepository : IMeasurementRepository
    {
        private CookbookDbContext dbContext;

        public EfMeasurementRepository(CookbookDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task AddAsync(Measurement measurement)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int measurementId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Measurement>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Measurement> GetByIdAsync(int measurementId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Measurement measurement)
        {
            throw new NotImplementedException();
        }
    }
}
