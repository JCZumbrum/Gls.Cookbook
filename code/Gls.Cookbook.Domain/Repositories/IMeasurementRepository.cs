﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.Domain.Models;

namespace Gls.Cookbook.Domain.Repositories
{
    public interface IMeasurementRepository
    {
        Task AddAsync(Measurement measurement);
        Task<Measurement> GetByIdAsync(int measurementId);
        Task<Measurement> GetByNameAsync(string name);
        Task<List<Measurement>> GetAllAsync();
        Task UpdateAsync(Measurement measurement);
        Task DeleteAsync(int measurementId);
    }
}
