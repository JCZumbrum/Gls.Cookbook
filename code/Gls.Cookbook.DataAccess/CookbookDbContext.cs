using System;
using System.IO;
using System.Threading.Tasks;
using Gls.Cookbook.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Gls.Cookbook.DataAccess
{
    public class CookbookDbContext : DbContext
    {
        private const string dbName = "Cookbook.db3";

        private string sourcePath;

        public DbSet<RecipeEntity> Recipes { get; set; }
        public DbSet<RecipeNoteEntity> RecipeNotes { get; set; }
        public DbSet<RecipeSectionEntity> RecipeSections { get; set; }
        public DbSet<RecipeIngredientSectionEntity> RecipeIngredientSections { get; set; }
        public DbSet<RecipeDirectionSectionEntity> RecipeDirectionSections { get; set; }
        public DbSet<RecipeIngredientEntity> RecipeIngredients { get; set; }
        public DbSet<RecipeDirectionEntity> RecipeDirections { get; set; }

        public DbSet<IngredientEntity> Ingredients { get; set; }
        public DbSet<MeasurementEntity> Measurements { get; set; }

        private CookbookDbContext(string sourcePath) { this.sourcePath = sourcePath; }

        public static CookbookDbContext Create()
        {
            return Create(String.Empty);
        }

        public static CookbookDbContext Create(string sourcePath)
        {
            CookbookDbContext cookbookDbContext = new CookbookDbContext(sourcePath);

            return cookbookDbContext;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string filename = Path.Combine(sourcePath, dbName);

            optionsBuilder.UseSqlite($"Data Source={filename}");

            base.OnConfiguring(optionsBuilder);
        }

        public static void Migrate(string sourcePath)
        {
            CookbookDbContext dbContext = Create(sourcePath);
            dbContext.Database.Migrate();
        }
    }
}