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
    public class EfRecipeRepository : IRecipeRepository
    {
        private CookbookDbContext dbContext;

        public EfRecipeRepository(CookbookDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(Recipe recipe)
        {
            RecipeEntity recipeEntity = recipe.MapToEntity();
            await dbContext.Recipes.AddAsync(recipeEntity);
            await dbContext.SaveChangesAsync();
        }

        public Task Delete(int recipeId)
        {
            throw new NotImplementedException();
        }

        public async Task<Recipe> GetByIdAsync(int recipeId)
        {
            RecipeEntity entity = await dbContext.Recipes.FirstOrDefaultAsync(r => r.Id == recipeId);
            return entity.MapToRecipe();
        }

        public Task UpdateAsync(Recipe recipe)
        {
            throw new NotImplementedException();
        }
    }
}
