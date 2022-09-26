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
        public static RecipeIngredientEntity MapToEntity(this RecipeIngredient recipeIngredient, RecipeSectionEntity recipeSectionEntity)
        {
            if (recipeIngredient == null)
                return null;

            return new RecipeIngredientEntity()
            {
                Id = recipeIngredient.Id,
                RecipeSection = recipeSectionEntity,
                Index = recipeIngredient.Index,
                IngredientId = recipeIngredient.IngredientId,
                MeasurementId = recipeIngredient.MeasurementId,
                QuantityText = recipeIngredient.QuantityText,
                Quantity = recipeIngredient.Quantity,
                Note = recipeIngredient.Note
            };
        }
    }
}
