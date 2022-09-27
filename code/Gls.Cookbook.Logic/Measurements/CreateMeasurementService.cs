using System.Threading.Tasks;
using Gls.Cookbook.Domain;
using Gls.Cookbook.Domain.Commands.Measurements;
using Gls.Cookbook.Domain.Models;
using Gls.Cookbook.Domain.Repositories;

namespace Gls.Cookbook.Logic.Measurements
{
    public class CreateMeasurementService : ICommandService<CreateMeasurementCommand, Measurement>
    {
        private ICookbookContextFactory cookbookContextFactory;

        public CreateMeasurementService(ICookbookContextFactory cookbookContextFactory)
        {
            this.cookbookContextFactory = cookbookContextFactory;
        }

        public async Task<Result<Measurement>> ExecuteAsync(CreateMeasurementCommand command)
        {
            await using (ICookbookContext cookbookContext = cookbookContextFactory.Create())
            {
                Measurement existingMeasurement = await cookbookContext.MeasurementRepository.GetByNameTypeAndSystemAsync(command.Name, command.MeasurementType, command.MeasurementSystem);
                if (existingMeasurement != null)
                    return Result<Measurement>.Fail($"Measurement with name {command.Name} already exists for type {command.MeasurementType} and system {command.MeasurementSystem}.");

                Measurement measurement = new Measurement()
                {
                    Name = command.Name,
                    Abbreviation = command.Abbreviation,
                    MeasurementType = command.MeasurementType,
                    MeasurementSystem = command.MeasurementSystem
                };

                int measurementId = await cookbookContext.MeasurementRepository.AddAsync(measurement);
                Measurement newMeasurement = await cookbookContext.MeasurementRepository.GetByIdAsync(measurementId);

                return Result<Measurement>.Pass(newMeasurement);
            }
        }
    }
}