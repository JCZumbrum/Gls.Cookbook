using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.Domain.Models;

namespace Gls.Cookbook.Domain.Queries
{
    public interface IQueryMeasurementService
    {
        Task<List<Measurement>> GetAllAsync();
        Task<Measurement> GetByIdAsync(int measurementId);
        Task<List<Measurement>> GetByTypeAsync(MeasurementType measurementType);
    }
}
