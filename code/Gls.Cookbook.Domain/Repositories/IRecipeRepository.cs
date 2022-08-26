﻿using System;
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
        Task<List<RecipeHeader>> GetHeaders();
        Task<Recipe> GetByIdAsync(int recipeId);
        Task<Recipe> GetByNameAsync(string name);
        Task UpdateAsync(Recipe recipe);
        Task DeleteAsync(int recipeId);
    }
}
