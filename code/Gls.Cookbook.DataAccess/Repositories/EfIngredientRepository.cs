using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.Domain.Models;
using Gls.Cookbook.Domain.Repositories;

namespace Gls.Cookbook.DataAccess.Repositories
{
    public class EfIngredientRepository : IIngredientRepository
    {
        private CookbookDbContext dbContext;

        public EfIngredientRepository(CookbookDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task AddAsync(Ingredient ingredient)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int ingredientId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Ingredient>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Ingredient> GetByIdAsync(int ingredientId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Ingredient ingredient)
        {
            throw new NotImplementedException();
        }
    }
}
