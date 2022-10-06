using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.Domain.Models;

namespace Gls.Cookbook.DataAccess.Models
{
    public static class RecipeIngredientSectionEntityExtensions
    {
        public static RecipeIngredientSectionEntity MapToEntity(this RecipeIngredientSection recipeIngredientSection, RecipeSectionEntity recipeSectionEntity)
        {
            if (recipeIngredientSection == null)
                return null;

            RecipeIngredientSectionEntity entity = new RecipeIngredientSectionEntity()
            {
                Id = recipeIngredientSection.Id,
                RecipeSection = recipeSectionEntity,
                Index = recipeIngredientSection.Index,
                Name = recipeIngredientSection.Name
            };

            entity.Ingredients = new List<RecipeIngredientEntity>(
                recipeIngredientSection.Ingredients.Select(
                    i => i.MapToEntity(entity)));

            return entity;
        }

        public static RecipeIngredientSection MapToDomain(this RecipeIngredientSectionEntity entity)
        {
            if (entity == null)
                return null;

            RecipeIngredientSection recipeIngredientSection = new RecipeIngredientSection()
            {
                Id = entity.Id,
                Index = entity.Index,
                Name = entity.Name
            };

            recipeIngredientSection.Ingredients = new List<RecipeIngredient>(
                entity.Ingredients.Select(
                    i => i.MapToDomain()));

            return recipeIngredientSection;
        }
    }
}
