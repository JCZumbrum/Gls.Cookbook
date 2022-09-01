using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gls.Cookbook.Domain.Repositories
{
    public interface ICookbookContext
    {
        IRecipeRepository RecipeRepository { get; }
        IIngredientRepository IngredientRepository { get; }
        IMeasurementRepository MeasurementRepository { get; }
        IRecipeTagRepository RecipeTagRepository { get; }

        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
