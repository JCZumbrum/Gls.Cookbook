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

        private static bool migrated = false;

        private string sourcePath;

        public DbSet<RecipeEntity> Recipes { get; set; }
        public DbSet<RecipeNoteEntity> RecipeNotes { get; set; }
        public DbSet<RecipeSectionEntity> RecipeSections { get; set; }
        public DbSet<RecipeIngredientEntity> RecipeIngredients { get; set; }
        public DbSet<RecipeDirectionEntity> RecipeInstructions { get; set; }
        public DbSet<RecipeTagEntity> RecipeTags { get; set; }

        public DbSet<IngredientEntity> Ingredients { get; set; }
        public DbSet<MeasurementEntity> Measurements { get; set; }

        private CookbookDbContext(string sourcePath) { this.sourcePath = sourcePath; }

        public static async Task<CookbookDbContext> CreateAsync()
        {
            return await CreateAsync(String.Empty);
        }

        public static async Task<CookbookDbContext> CreateAsync(string sourcePath)
        {
            CookbookDbContext cookbookDbContext = new CookbookDbContext(sourcePath);

            if (!migrated)
            {
                await cookbookDbContext.Database.MigrateAsync();
                migrated = true;
            }

            return cookbookDbContext;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string filename = Path.Combine(sourcePath, dbName);

            optionsBuilder.UseSqlite($"Data Source={filename}");

            base.OnConfiguring(optionsBuilder);
        }
    }
}