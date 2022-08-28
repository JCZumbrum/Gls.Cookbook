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
            return new RecipeIngredientEntity()
            {
                Id = recipeIngredient.Id,
                RecipeSection = recipeSectionEntity,
                IngredientId = recipeIngredient.IngredientId,
                MeasurementId = recipeIngredient.MeasurementId,
                Quantity = recipeIngredient.Quantity,
                Note = recipeIngredient.Note
            };
        }
    }
}
