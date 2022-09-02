using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.Domain.Models;

namespace Gls.Cookbook.Domain.Repositories
{
    public interface IRecipeTagRepository
    {
        Task<List<RecipeTag>> GetAllAsync();
    }
}
