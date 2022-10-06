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
            if (recipeSection == null)
                return null;

            RecipeSectionEntity entity = new RecipeSectionEntity()
            {
                Id = recipeSection.Id,
                Recipe = recipeEntity,
                Index = recipeSection.Index,
                Name = recipeSection.Name
            };

            entity.IngredientSections = new List<RecipeIngredientSectionEntity>(
                recipeSection.IngredientSections.Select(
                    s => s.MapToEntity(entity)));

            entity.DirectionSections = new List<RecipeDirectionSectionEntity>(
                recipeSection.DirectionSections.Select(
                    s => s.MapToEntity(entity)));

            return entity;
        }

        public static RecipeSection MapToDomain(this RecipeSectionEntity entity)
        {
            if (entity == null)
                return null;

            RecipeSection recipeSection = new RecipeSection()
            {
                Id = entity.Id,
                Index = entity.Index,
                Name = entity.Name
            };

            recipeSection.IngredientSections = new List<RecipeIngredientSection>(
                entity.IngredientSections.Select(
                    s => s.MapToDomain()));

            recipeSection.DirectionSections = new List<RecipeDirectionSection>(
                entity.DirectionSections.Select(
                    s => s.MapToDomain()));

            return recipeSection;
        }
    }
}
