using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.Domain;
using Gls.Cookbook.Domain.Commands.Ingredients;
using Gls.Cookbook.Domain.Models;
using Gls.Cookbook.Domain.Repositories;

namespace Gls.Cookbook.Logic.Ingredients
{
    public class CreateIngredientService : ICommandService<CreateIngredientCommand, Ingredient>
    {
        private ICookbookContextFactory cookbookContextFactory;

        public CreateIngredientService(ICookbookContextFactory cookbookContextFactory)
        {
            this.cookbookContextFactory = cookbookContextFactory;
        }

        public async Task<Result<Ingredient>> ExecuteAsync(CreateIngredientCommand command)
        {
            if (String.IsNullOrEmpty(command.Name))
                return Result<Ingredient>.Fail("Ingredient name is required.");

            await using (ICookbookContext cookbookContext = cookbookContextFactory.Create())
            {
                Ingredient existingIngredient = await cookbookContext.IngredientRepository.GetByNameAsync(command.Name);
                if (existingIngredient != null)
                    return Result<Ingredient>.Fail($"Ingredient with name {command.Name} already exists.");

                Ingredient ingredient = new Ingredient()
                {
                    Name = command.Name,
                    Note = command.Note
                };

                int ingredientId = await cookbookContext.IngredientRepository.AddAsync(ingredient);
                Ingredient newIngredient = await cookbookContext.IngredientRepository.GetByIdAsync(ingredientId);

                return Result<Ingredient>.Pass(newIngredient);
            }
        }
    }
}
