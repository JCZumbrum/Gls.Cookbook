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
    public class UpdateIngredientService : ICommandService<UpdateIngredientCommand, Ingredient>
    {
        private ICookbookContextFactory cookbookContextFactory;

        public UpdateIngredientService(ICookbookContextFactory cookbookContextFactory)
        {
            this.cookbookContextFactory = cookbookContextFactory;
        }

        public async Task<Result<Ingredient>> ExecuteAsync(UpdateIngredientCommand command)
        {
            await using (ICookbookContext cookbookContext = cookbookContextFactory.Create())
            {
                Ingredient existingIngredient = await cookbookContext.IngredientRepository.GetByNameAsync(command.Name);
                if (existingIngredient == null)
                    return Result<Ingredient>.Fail("Ingredient does not exist.");

                existingIngredient.Name = command.Name;
                existingIngredient.Note = command.Note;

                await cookbookContext.IngredientRepository.UpdateAsync(existingIngredient);

                Ingredient updatedIngredient = await cookbookContext.IngredientRepository.GetByIdAsync(existingIngredient.Id);

                return Result<Ingredient>.Pass(updatedIngredient);
            }
        }
    }
}
