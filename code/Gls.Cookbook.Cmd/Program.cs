using System.Diagnostics.Metrics;
using System.Net.NetworkInformation;
using Gls.Cookbook.DataAccess;
using Gls.Cookbook.Domain.Models;
using Gls.Cookbook.Domain.Repositories;

namespace Gls.Cookbook.Cmd
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //await CreateIngredients();
            await CreateRecipe();
            //await DeleteRecipe();
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
            Ingredient flourIngredient;
            Measurement cupMeasurement;

            await using (EfCookbookContext cookbookContext = await EfCookbookContext.CreateAsync())
            {
                flourIngredient = await cookbookContext.IngredientRepository.GetByNameAsync("Flour");
                cupMeasurement = await cookbookContext.MeasurementRepository.GetByNameAsync("Cup");
            }

            Recipe recipe = new Recipe() { Name = "Bread" };
            recipe.Sections.Add(new RecipeSection()
            {
                Name = "Main",
                Instructions = new List<RecipeInstruction>() { new RecipeInstruction() { LineNumber = 1, Instruction = "Knead dough." } },
                Ingredients = new List<RecipeIngredient>()
                {
                    new RecipeIngredient() { Ingredient = flourIngredient, Measurement = cupMeasurement, Quantity = 1 },
                    new RecipeIngredient() { Ingredient = flourIngredient, Measurement = cupMeasurement, Quantity = 1 }
                }
            });

            await using (EfCookbookContext cookbookContext = await EfCookbookContext.CreateAsync())
            {
                await cookbookContext.RecipeRepository.AddAsync(recipe);
            }
        }
    }
}