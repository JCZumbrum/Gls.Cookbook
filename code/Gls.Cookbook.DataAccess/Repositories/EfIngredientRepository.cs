using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.DataAccess.Models;
using Gls.Cookbook.Domain.Models;
using Gls.Cookbook.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gls.Cookbook.DataAccess.Repositories
{
    public class EfIngredientRepository : IIngredientRepository
    {
        private CookbookDbContext dbContext;

        public EfIngredientRepository(CookbookDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(Ingredient ingredient)
        {
            IngredientEntity ingredientEntity = ingredient.MapToEntity();
            await dbContext.Ingredients.AddAsync(ingredientEntity);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int ingredientId)
        {
            IngredientEntity ingredientEntity = await dbContext.Ingredients.FirstOrDefaultAsync(r => r.Id == ingredientId);
            if (ingredientEntity == null)
                return;

            dbContext.Ingredients.Remove(ingredientEntity);

            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Ingredient>> GetAll()
        {
            return await dbContext.Ingredients.Select(e => e.MapToIngredient()).ToListAsync();
        }

        public async Task<Ingredient> GetByIdAsync(int ingredientId)
        {
            IngredientEntity ingredientEntity = await dbContext.Ingredients.FirstOrDefaultAsync(i => i.Id == ingredientId);
            return ingredientEntity.MapToIngredient();
        }

        public async Task UpdateAsync(Ingredient ingredient)
        {
            IngredientEntity ingredientEntity = await dbContext.Ingredients.FirstOrDefaultAsync(i => i.Id == ingredient.Id);

            ingredientEntity.Name = ingredient.Name;
            ingredientEntity.Description = ingredient.Description;

            dbContext.Ingredients.Update(ingredientEntity);
            await dbContext.SaveChangesAsync();
        }
    }
}
