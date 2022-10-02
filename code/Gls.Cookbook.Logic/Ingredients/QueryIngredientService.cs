using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.Domain.Models;
using Gls.Cookbook.Domain.Queries;
using Gls.Cookbook.Domain.Repositories;

namespace Gls.Cookbook.Logic.Ingredients
{
    public class QueryIngredientService : IQueryIngredientService
    {
        private ICookbookContextFactory cookbookContextFactory;

        public QueryIngredientService(ICookbookContextFactory cookbookContextFactory)
        {
            this.cookbookContextFactory = cookbookContextFactory;
        }

        public async Task<List<Ingredient>> GetAllAsync()
        {
            await using (ICookbookContext cookbookContext = cookbookContextFactory.Create())
            {
                return await cookbookContext.IngredientRepository.GetAllAsync();
            }
        }
    }
}
