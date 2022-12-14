using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.Domain;
using Gls.Cookbook.Domain.Commands.Measurements;
using Gls.Cookbook.Domain.Models;
using Gls.Cookbook.Domain.Repositories;

namespace Gls.Cookbook.Logic.Measurements
{
    public class UpdateMeasurementService : ICommandService<UpdateMeasurementCommand, Measurement>
    {
        private ICookbookContextFactory cookbookContextFactory;

        public UpdateMeasurementService(ICookbookContextFactory cookbookContextFactory)
        {
            this.cookbookContextFactory = cookbookContextFactory;
        }

        public async Task<Result<Measurement>> ExecuteAsync(UpdateMeasurementCommand command)
        {
            if (String.IsNullOrEmpty(command.Name))
                return Result<Measurement>.Fail("Measurement name is required.");

            await using (ICookbookContext cookbookContext = cookbookContextFactory.Create())
            {
                Measurement existingMeasurement = await cookbookContext.MeasurementRepository.GetByIdAsync(command.Id);
                if (existingMeasurement == null)
                    return Result<Measurement>.Fail("Measurement does not exist.");

                existingMeasurement.Name = command.Name;
                existingMeasurement.Abbreviation = command.Abbreviation;
                existingMeasurement.MeasurementType = command.MeasurementType;
                existingMeasurement.MeasurementSystem = command.MeasurementSystem;

                await cookbookContext.MeasurementRepository.UpdateAsync(existingMeasurement);

                Measurement updatedMeasurement = await cookbookContext.MeasurementRepository.GetByIdAsync(existingMeasurement.Id);

                return Result<Measurement>.Pass(updatedMeasurement);
            }
        }
    }
}
