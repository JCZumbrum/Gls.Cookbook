using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.Domain;
using Gls.Cookbook.Domain.Models;
using Gls.Cookbook.Domain.Queries;
using Gls.Cookbook.Domain.Repositories;

namespace Gls.Cookbook.Logic.Measurements
{
    public class QueryMeasurementService : IQueryMeasurementService
    {
        private ICookbookContextFactory cookbookContextFactory;

        public QueryMeasurementService(ICookbookContextFactory cookbookContextFactory)
        {
            this.cookbookContextFactory = cookbookContextFactory;
        }

        public async Task<List<Measurement>> GetAllAsync()
        {
            await using (ICookbookContext cookbookContext = await cookbookContextFactory.CreateAsync())
            {
                return await cookbookContext.MeasurementRepository.GetAllAsync();
            }
        }

        public async Task<List<Measurement>> GetByTypeAsync(MeasurementType measurementType)
        {
            await using (ICookbookContext cookbookContext = await cookbookContextFactory.CreateAsync())
            {
                return await cookbookContext.MeasurementRepository.GetByTypeAsync(measurementType);
            }
        }
    }
}
