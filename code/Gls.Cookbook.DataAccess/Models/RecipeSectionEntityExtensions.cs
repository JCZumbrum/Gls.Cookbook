using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.Domain.Models;

namespace Gls.Cookbook.DataAccess.Models
{
    public static class RecipeSectionEntityExtensions
    {
        public static RecipeSectionEntity MapToEntity(this RecipeSection recipeSection, RecipeEntity recipeEntity)
        {
            RecipeSectionEntity entity = new RecipeSectionEntity()
            {
                Id = recipeSection.Id,
                Recipe = recipeEntity,
                Index = recipeSection.Index,
                Name = recipeSection.Name
            };

            entity.Ingredients = new List<RecipeIngredientEntity>(
                recipeSection.Ingredients.Select(
                    i => i.MapToEntity(entity)));

            entity.Directions = new List<RecipeDirectionEntity>(
                recipeSection.Directions.Select(
                    i => new RecipeDirectionEntity()
                    {
                        Id = i.Id,
                        RecipeSection = entity,
                        Index = i.Index,
                        Direction = i.Direction,
                        Note = i.Note
                    }));

            return entity;
        }

        public static RecipeSection MapToRecipeSection(this RecipeSectionEntity entity)
        {
            RecipeSection recipeSection = new RecipeSection()
            {
                Id = entity.Id,
                Index = entity.Index,
                Name = entity.Name
            };

            recipeSection.Ingredients = new List<RecipeIngredient>(
                entity.Ingredients.Select(
                    i => new RecipeIngredient()
                    {
                        Id = i.Id,
                        Index = i.Index,
                        IngredientId = i.IngredientId,
                        MeasurementId = i.MeasurementId,
                        Note = i.Note
                    }));

            recipeSection.Directions = new List<RecipeDirection>(
                entity.Directions.Select(
                    i => new RecipeDirection()
                    {
                        Id = i.Id,
                        Index = i.Index,
                        Direction = i.Direction,
                        Note = i.Note
                    }));

            return recipeSection;
        }
    }
}
