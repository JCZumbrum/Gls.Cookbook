using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.DataAccess.Repositories;
using Gls.Cookbook.Domain.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Gls.Cookbook.DataAccess
{
    public class EfCookbookContext : ICookbookContext
    {
        private CookbookDbContext dbContext = null;
        private IRecipeRepository recipeRepository = null;
        private IIngredientRepository ingredientRepository = null;
        private IMeasurementRepository measurementRepository = null;

        private IDbContextTransaction transaction = null;

        public IRecipeRepository RecipeRepository
        {
            get
            {
                recipeRepository ??= new EfRecipeRepository(dbContext);

                return recipeRepository;
            }
        }

        public IIngredientRepository IngredientRepository
        {
            get
            {
                ingredientRepository ??= new EfIngredientRepository(dbContext);

                return ingredientRepository;
            }
        }

        public IMeasurementRepository MeasurementRepository
        {
            get
            {
                measurementRepository ??= new EfMeasurementRepository(dbContext);

                return measurementRepository;
            }
        }

        private EfCookbookContext(CookbookDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public static async Task<EfCookbookContext> CreateAsync()
        {
            CookbookDbContext dbContext = await CookbookDbContext.CreateAsync();
            return new EfCookbookContext(dbContext);
        }

        public async Task BeginTransactionAsync()
        {
            if (transaction != null)
                throw new InvalidOperationException("Cannot begin, there is already an active transaction.");

            transaction = await dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (transaction == null)
                throw new InvalidOperationException("Cannot commit, there is no active transaction.");

            await transaction.CommitAsync();
            transaction = null;
        }

        public async Task RollbackTransactionAsync()
        {
            if (transaction == null)
                throw new InvalidOperationException("Cannot rollback, there is no active transaction.");

            await transaction.RollbackAsync();
            transaction = null;
        }

        public async ValueTask DisposeAsync()
        {
            if (transaction != null)
                await transaction.RollbackAsync();

            await dbContext.DisposeAsync();
        }
    }
}
