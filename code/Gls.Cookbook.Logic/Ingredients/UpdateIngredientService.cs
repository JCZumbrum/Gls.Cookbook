using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.Domain;
using Gls.Cookbook.Domain.Commands.Ingredients;
using Gls.Cookbook.Domain.Models;
using Gls.Cookbook.Domain.Repositories;

namespace Gls.Cookbook.Logic.Ingredients
{
    public class UpdateIngredientService : ICommandService<UpdateIngredientCommand>
    {
        private ICookbookContextFactory cookbookContextFactory;

        public UpdateIngredientService(ICookbookContextFactory cookbookContextFactory)
        {
            this.cookbookContextFactory = cookbookContextFactory;
        }

        public async Task<Result> ExecuteAsync(UpdateIngredientCommand command)
        {
            await using (ICookbookContext cookbookContext = cookbookContextFactory.Create())
            {
                Ingredient existingIngredient = await cookbookContext.IngredientRepository.GetByNameAsync(command.Name);
                if (existingIngredient == null)
                    return Result.Fail("Ingredient does not exist.");

                existingIngredient.Name = command.Name;
                existingIngredient.Description = command.Description;

                await cookbookContext.IngredientRepository.UpdateAsync(existingIngredient);

                return Result.Pass();
            }
        }
    }
}
