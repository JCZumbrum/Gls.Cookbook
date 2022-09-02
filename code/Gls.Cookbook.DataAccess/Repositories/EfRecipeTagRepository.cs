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
    public class EfRecipeTagRepository : IRecipeTagRepository
    {
        private CookbookDbContext dbContext;

        public EfRecipeTagRepository(CookbookDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<List<RecipeTag>> GetAllAsync()
        {
            dbContext.RecipeTags.FromSqlRaw("SELECT json_each(Tags) FROM Recipes");

            return null;
        }
    }
}
