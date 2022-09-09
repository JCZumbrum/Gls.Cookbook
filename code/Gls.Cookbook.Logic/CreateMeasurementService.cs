using System.Threading.Tasks;
using Gls.Cookbook.Domain;
using Gls.Cookbook.Domain.Commands;
using Gls.Cookbook.Domain.Models;
using Gls.Cookbook.Domain.Repositories;

namespace Gls.Cookbook.Logic
{
    public class CreateMeasurementService : ICommandService<CreateMeasurementCommand>
    {
        private ICookbookContextFactory cookbookContextFactory;

        public CreateMeasurementService(ICookbookContextFactory cookbookContextFactory)
        {
            this.cookbookContextFactory = cookbookContextFactory;
        }

        public async Task ExecuteAsync(CreateMeasurementCommand command)
        {
            await using (ICookbookContext cookbookContext = await cookbookContextFactory.CreateAsync())
            {
                Measurement existingMeasurement = await cookbookContext.MeasurementRepository.GetByNameAsync(command.Name);
                if (existingMeasurement == null)
                    throw new System.Exception();

                Measurement measurement = new Measurement()
                {
                    Name = command.Name,
                    Abbreviation = command.Abbreviation,
                    MeasurementType = command.MeasurementType,
                    MeasurementSystem = command.MeasurementSystem
                };

                await cookbookContext.MeasurementRepository.AddAsync(measurement);
            }
        }
    }
}