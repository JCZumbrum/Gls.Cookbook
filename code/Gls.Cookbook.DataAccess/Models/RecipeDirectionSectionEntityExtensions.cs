using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.Domain.Models;

namespace Gls.Cookbook.DataAccess.Models
{
    public static class RecipeDirectionSectionEntityExtensions
    {
        public static RecipeDirectionSectionEntity MapToEntity(this RecipeDirectionSection recipeDirectionSection, RecipeSectionEntity recipeSectionEntity)
        {
            if (recipeDirectionSection == null)
                return null;

            RecipeDirectionSectionEntity entity = new RecipeDirectionSectionEntity()
            {
                Id = recipeDirectionSection.Id,
                RecipeSection = recipeSectionEntity,
                Index = recipeDirectionSection.Index,
                Name = recipeDirectionSection.Name
            };

            entity.Directions = new List<RecipeDirectionEntity>(
                recipeDirectionSection.Directions.Select(
                    d => d.MapToEntity(entity)));

            return entity;
        }

        public static RecipeDirectionSection MapToDomain(this RecipeDirectionSectionEntity entity)
        {
            if (entity == null)
                return null;

            RecipeDirectionSection recipeDirectionSection = new RecipeDirectionSection()
            {
                Id = entity.Id,
                Index = entity.Index,
                Name = entity.Name
            };

            recipeDirectionSection.Directions = new List<RecipeDirection>(
                entity.Directions.Select(
                    d => d.MapToDomain()));

            return recipeDirectionSection;
        }
    }
}
