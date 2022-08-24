using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.Domain.Models;

namespace Gls.Cookbook.DataAccess.Models
{
    public static class RecipeEntityExtensions
    {
        public static RecipeEntity MapToEntity(this Recipe recipe)
        {
            RecipeEntity entity = new RecipeEntity()
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Description = recipe.Description
            };
            entity.Notes = recipe.Notes.Select(
                    n => new RecipeNoteEntity()
                    {
                        Id = n.Id,
                        Recipe = entity,
                        Note = n.Note
                    }).ToList();
            entity.Sections = recipe.Sections.Select(
                    s => s.MapToEntity(entity)).ToList();

            return entity;
        }

        public static Recipe MapToRecipe(this RecipeEntity entity)
        {
            if (entity == null)
                return null;

            Recipe recipe = new Recipe()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };
            recipe.Notes = entity.Notes.Select(
                    n => new RecipeNote
                    {
                        Id = n.Id,
                        Note = n.Note
                    }).ToList();
            recipe.Sections = entity.Sections.Select(
                    s => s.MapToRecipeSection()).ToList();

            return recipe;
        }
    }
}
