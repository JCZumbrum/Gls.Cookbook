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

        public async Task<int> AddAsync(Ingredient ingredient)
        {
            IngredientEntity ingredientEntity = ingredient.MapToEntity();
            await dbContext.Ingredients.AddAsync(ingredientEntity);
            await dbContext.SaveChangesAsync();

            return ingredientEntity.Id;
        }

        public async Task DeleteAsync(int ingredientId)
        {
            IngredientEntity ingredientEntity = await dbContext.Ingredients.FirstOrDefaultAsync(r => r.Id == ingredientId);
            if (ingredientEntity == null)
                return;

            dbContext.Ingredients.Remove(ingredientEntity);

            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Ingredient>> GetAllAsync()
        {
            return await dbContext.Ingredients.Select(e => e.MapToDomain()).ToListAsync();
        }

        public async Task<Ingredient> GetByIdAsync(int ingredientId)
        {
            IngredientEntity ingredientEntity = await dbContext.Ingredients.FirstOrDefaultAsync(i => i.Id == ingredientId);
            return ingredientEntity.MapToDomain();
        }

        public async Task<Ingredient> GetByNameAsync(string name)
        {
            IngredientEntity ingredientEntity = await dbContext.Ingredients.FirstOrDefaultAsync(i => i.Name == name);
            return ingredientEntity.MapToDomain();
        }

        public async Task UpdateAsync(Ingredient ingredient)
        {
            IngredientEntity ingredientEntity = await dbContext.Ingredients.FirstOrDefaultAsync(i => i.Id == ingredient.Id);

            ingredientEntity.Name = ingredient.Name;
            ingredientEntity.Note = ingredient.Note;

            dbContext.Ingredients.Update(ingredientEntity);
            await dbContext.SaveChangesAsync();
        }
    }
}
