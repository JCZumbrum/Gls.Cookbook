using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.Domain;
using Gls.Cookbook.Domain.Commands.Measurements;
using Gls.Cookbook.Domain.Repositories;

namespace Gls.Cookbook.Logic.Measurements
{
    public class DeleteMeasurementService : ICommandService<DeleteMeasurementCommand>
    {
        private ICookbookContextFactory cookbookContextFactory;

        public DeleteMeasurementService(ICookbookContextFactory cookbookContextFactory)
        {
            this.cookbookContextFactory = cookbookContextFactory;
        }

        public async Task<Result> ExecuteAsync(DeleteMeasurementCommand command)
        {
            await using (ICookbookContext cookbookContext = await cookbookContextFactory.CreateAsync())
            {
                bool recipeExists = await cookbookContext.RecipeRepository.ExistsByMeasurementId(command.Id);
                if (recipeExists)
                    return Result.Fail("Measurement is in use.");

                await cookbookContext.MeasurementRepository.DeleteAsync(command.Id);

                return Result.Pass();
            }
        }
    }
}
