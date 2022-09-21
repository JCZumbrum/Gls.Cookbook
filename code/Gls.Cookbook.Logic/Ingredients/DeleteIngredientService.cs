using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.Domain;
using Gls.Cookbook.Domain.Commands.Ingredients;
using Gls.Cookbook.Domain.Repositories;

namespace Gls.Cookbook.Logic.Ingredients
{
    public class DeleteIngredientService : ICommandService<DeleteIngredientCommand>
    {
        private ICookbookContextFactory cookbookContextFactory;

        public DeleteIngredientService(ICookbookContextFactory cookbookContextFactory)
        {
            this.cookbookContextFactory = cookbookContextFactory;
        }

        public async Task<Result> ExecuteAsync(DeleteIngredientCommand command)
        {
            await using (ICookbookContext cookbookContext = cookbookContextFactory.Create())
            {
                bool recipeExists = await cookbookContext.RecipeRepository.ExistsByIngredientId(command.Id);
                if (recipeExists)
                    return Result.Fail("Ingredient is in use.");

                await cookbookContext.IngredientRepository.DeleteAsync(command.Id);

                return Result.Pass();
            }
        }
    }
}
