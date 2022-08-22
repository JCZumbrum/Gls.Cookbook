using System.Threading.Tasks;
using Gls.Cookbook.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Gls.Cookbook.DataAccess
{
    public class CookbookDbContext : DbContext
    {
        private static bool migrated = false;

        public DbSet<RecipeEntity> Recipes { get; set; }
        public DbSet<RecipeNoteEntity> RecipeNotes { get; set; }
        public DbSet<RecipeSectionEntity> RecipeSections { get; set; }
        public DbSet<RecipeIngredientEntity> RecipeIngredients { get; set; }
        public DbSet<RecipeInstructionEntity> RecipeInstructions { get; set; }

        public DbSet<IngredientEntity> Ingredients { get; set; }
        public DbSet<MeasurementEntity> Measurements { get; set; }

        private CookbookDbContext() { }

        public static async Task<CookbookDbContext> CreateAsync()
        {
            CookbookDbContext cookbookDbContext = new CookbookDbContext();

            if (!migrated)
            {
                await cookbookDbContext.Database.MigrateAsync();
                migrated = true;
            }

            return cookbookDbContext;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Cookbook.db3");

            base.OnConfiguring(optionsBuilder);
        }
    }
}