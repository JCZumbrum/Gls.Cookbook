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
    public class CreateIngredientService : ICommandService<CreateIngredientCommand>
    {
        private ICookbookContextFactory cookbookContextFactory;

        public CreateIngredientService(ICookbookContextFactory cookbookContextFactory)
        {
            this.cookbookContextFactory = cookbookContextFactory;
        }

        public async Task<Result> ExecuteAsync(CreateIngredientCommand command)
        {
            await using (ICookbookContext cookbookContext = cookbookContextFactory.Create())
            {
                Ingredient existingIngredient = await cookbookContext.IngredientRepository.GetByNameAsync(command.Name);
                if (existingIngredient != null)
                    return Result.Fail("Ingredient with that name already exists.");

                Ingredient ingredient = new Ingredient()
                {
                    Name = command.Name,
                    Note = command.Note
                };

                await cookbookContext.IngredientRepository.AddAsync(ingredient);

                return Result.Pass();
            }
        }
    }
}
