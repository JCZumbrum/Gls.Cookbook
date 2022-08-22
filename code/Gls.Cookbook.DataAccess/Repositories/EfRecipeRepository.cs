using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public Task AddAsync(Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int recipeId)
        {
            throw new NotImplementedException();
        }

        public Task<Recipe> GetByIdAsync(int recipeId)
        {
            throw new NotImplementedException();
            //dbContext.Recipes.FirstOrDefaultAsync(r => r.Id == recipeId);
        }

        public Task UpdateAsync(Recipe recipe)
        {
            throw new NotImplementedException();
        }
    }
}
