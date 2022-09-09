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
        Task<List<RecipeHeader>> GetHeadersAsync();
        Task<List<Recipe>> GetAllAsync();
        Task<List<(string Tag, int Count)>> GetAllTagsAsync();
        Task<Recipe> GetByIdAsync(int recipeId);
        Task<Recipe> GetByNameAsync(string name);
        Task UpdateAsync(Recipe recipe);
        Task DeleteAsync(int recipeId);
        Task<bool> ExistsByMeasurementId(int measurementId);
    }
}
