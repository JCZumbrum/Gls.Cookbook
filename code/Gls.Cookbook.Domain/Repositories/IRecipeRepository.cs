using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.Domain.Models;

namespace Gls.Cookbook.Domain.Repositories
{
    public interface IRecipeRepository
    {
        Task AddAsync(Recipe recipe);
        Task<Recipe> GetByIdAsync(int recipeId);
        Task UpdateAsync(Recipe recipe);
        Task Delete(int recipeId);
    }
}
