using System.Diagnostics.Metrics;
using System.Net.NetworkInformation;
using Gls.Cookbook.DataAccess;
using Gls.Cookbook.Domain;
using Gls.Cookbook.Domain.Models;
using Gls.Cookbook.Domain.Repositories;

namespace Gls.Cookbook.Cmd
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //await CreateIngredients();
            //await CreateRecipe();
            await RetrieveRecipe();
            //await UpdateRecipe();
            //await DeleteRecipe();
        }

        private static async Task RetrieveRecipe()
        {
            //List<Recipe> recipes;
            await using (EfCookbookContext cookbookContext = await EfCookbookContext.CreateAsync())
            {
                //recipes = await cookbookContext.RecipeRepository.GetAllAsync();

                var tags = await cookbookContext.RecipeRepository.GetAllTagsAsync();
            }
        }

        static async Task CreateIngredients()
        {
            ICookbookContext cookbookContext = await EfCookbookContext.CreateAsync();

            await cookbookContext.IngredientRepository.AddAsync(new Ingredient() { Name = "Flour" });
            await cookbookContext.IngredientRepository.AddAsync(new Ingredient() { Name = "Water" });
        }

        static async Task DeleteRecipe()
        {
            Recipe recipe;
            await using (EfCookbookContext cookbookContext = await EfCookbookContext.CreateAsync())
            {
                recipe = await cookbookContext.RecipeRepository.GetByNameAsync("Bread");
            }

            await using (EfCookbookContext cookbookContext = await EfCookbookContext.CreateAsync())
            {
                await cookbookContext.RecipeRepository.DeleteAsync(recipe.Id);
            }
        }

        static async Task CreateRecipe()
        {
            //Ingredient flourIngredient;
            //Ingredient waterIngredient;
            //Measurement cupMeasurement;
            //Measurement fluidOunceMeasurement;

            //await using (EfCookbookContext cookbookContext = await EfCookbookContext.CreateAsync())
            //{
            //    flourIngredient = await cookbookContext.IngredientRepository.GetByNameAsync("Flour");
            //    waterIngredient = await cookbookContext.IngredientRepository.GetByNameAsync("Water");
            //    cupMeasurement = await cookbookContext.MeasurementRepository.GetByNameAsync("Cup");
            //    fluidOunceMeasurement = await cookbookContext.MeasurementRepository.GetByNameAsync("Fluid Ounce");
            //}

            Recipe breadRecipe = new Recipe() { Name = "Bread" };
            breadRecipe.Tags = new List<string>() { "Baking" };

            await using (EfCookbookContext cookbookContext = await EfCookbookContext.CreateAsync())
            {
                await cookbookContext.RecipeRepository.AddAsync(breadRecipe);
            }

            Recipe cakeRecipe = new Recipe() { Name = "Carrot Cake" };
            cakeRecipe.Tags = new List<string>() { "Baking", "Dessert" };

            await using (EfCookbookContext cookbookContext = await EfCookbookContext.CreateAsync())
            {
                await cookbookContext.RecipeRepository.AddAsync(cakeRecipe);
            }

            Recipe pieRecipe = new Recipe() { Name = "Cherry Pie" };
            pieRecipe.Tags = new List<string>() { "Baking", "Dessert", "Fruit" };

            await using (EfCookbookContext cookbookContext = await EfCookbookContext.CreateAsync())
            {
                await cookbookContext.RecipeRepository.AddAsync(pieRecipe);
            }
        }

        static async Task UpdateRecipe()
        {
            Ingredient flourIngredient;
            Ingredient waterIngredient;
            Measurement cupMeasurement;
            Measurement fluidOunceMeasurement;
            Recipe recipe;

            await using (EfCookbookContext cookbookContext = await EfCookbookContext.CreateAsync())
            {
                flourIngredient = await cookbookContext.IngredientRepository.GetByNameAsync("Flour");
                waterIngredient = await cookbookContext.IngredientRepository.GetByNameAsync("Water");
                cupMeasurement = await cookbookContext.MeasurementRepository.GetByNameTypeAndSystemAsync("Cup", MeasurementType.Volume, MeasurementSystem.UsCustomary);
                fluidOunceMeasurement = await cookbookContext.MeasurementRepository.GetByNameTypeAndSystemAsync("Fluid Ounce", MeasurementType.Volume, MeasurementSystem.UsCustomary);

                recipe = await cookbookContext.RecipeRepository.GetByNameAsync("Bread");
            }

            recipe.Sections[0].Directions.Add(new RecipeDirection() { Index = 2, Direction = "Activate yeast" });

            recipe.Sections[0].Ingredients[1] = new RecipeIngredient() { IngredientId = waterIngredient.Id, MeasurementId = fluidOunceMeasurement.Id, Quantity = 8 };

            await using (EfCookbookContext cookbookContext = await EfCookbookContext.CreateAsync())
            {
                await cookbookContext.RecipeRepository.UpdateAsync(recipe);
            }
        }
    }
}