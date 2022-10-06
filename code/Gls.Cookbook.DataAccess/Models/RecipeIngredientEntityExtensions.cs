using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.Domain.Models;

namespace Gls.Cookbook.DataAccess.Models
{
    public static class RecipeIngredientEntityExtensions
    {
        public static RecipeIngredientEntity MapToEntity(this RecipeIngredient recipeIngredient, RecipeIngredientSectionEntity recipeIngredientSectionEntity)
        {
            if (recipeIngredient == null)
                return null;

            return new RecipeIngredientEntity()
            {
                Id = recipeIngredient.Id,
                RecipeIngredientSection = recipeIngredientSectionEntity,
                Index = recipeIngredient.Index,
                MinimumQuantityText = recipeIngredient.MinimumQuantityText,
                MaximumQuantityText = recipeIngredient.MaximumQuantityText,
                MeasurementId = recipeIngredient.MeasurementId,
                IngredientId = recipeIngredient.IngredientId,
                Note = recipeIngredient.Note
            };
        }

        public static RecipeIngredient MapToDomain(this RecipeIngredientEntity entity)
        {
            if (entity == null)
                return null;

            return new RecipeIngredient()
            {
                Id = entity.Id,
                Index = entity.Index,
                MinimumQuantityText = entity.MinimumQuantityText,
                MaximumQuantityText = entity.MaximumQuantityText,
                MeasurementId = entity.MeasurementId,
                IngredientId = entity.IngredientId,
                Note = entity.Note
            };
        }
    }
}
