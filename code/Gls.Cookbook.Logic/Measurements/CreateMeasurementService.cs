using System.Threading.Tasks;
using Gls.Cookbook.Domain;
using Gls.Cookbook.Domain.Commands.Measurements;
using Gls.Cookbook.Domain.Models;
using Gls.Cookbook.Domain.Repositories;

namespace Gls.Cookbook.Logic.Measurements
{
    public class CreateMeasurementService : ICommandService<CreateMeasurementCommand>
    {
        private ICookbookContextFactory cookbookContextFactory;

        public CreateMeasurementService(ICookbookContextFactory cookbookContextFactory)
        {
            this.cookbookContextFactory = cookbookContextFactory;
        }

        public async Task<Result> ExecuteAsync(CreateMeasurementCommand command)
        {
            await using (ICookbookContext cookbookContext = cookbookContextFactory.Create())
            {
                Measurement existingMeasurement = await cookbookContext.MeasurementRepository.GetByNameTypeAndSystemAsync(command.Name, command.MeasurementType, command.MeasurementSystem);
                if (existingMeasurement != null)
                    return Result.Fail("Measurement with that name already exists.");

                Measurement measurement = new Measurement()
                {
                    Name = command.Name,
                    Abbreviation = command.Abbreviation,
                    MeasurementType = command.MeasurementType,
                    MeasurementSystem = command.MeasurementSystem
                };

                await cookbookContext.MeasurementRepository.AddAsync(measurement);

                return Result.Pass();
            }
        }
    }
}