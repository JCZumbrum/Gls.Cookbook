using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.Domain.Models;

namespace Gls.Cookbook.Domain.Repositories
{
    public interface IIngredientRepository
    {
        Task<int> AddAsync(Ingredient ingredient);
        Task<Ingredient> GetByNameAsync(string name);
        Task<Ingredient> GetByIdAsync(int ingredientId);
        Task<List<Ingredient>> GetAllAsync();
        Task UpdateAsync(Ingredient ingredient);
        Task DeleteAsync(int ingredientId);
    }
}
