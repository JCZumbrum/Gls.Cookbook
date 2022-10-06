using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.Domain.Models;

namespace Gls.Cookbook.DataAccess.Models
{
    public static class RecipeDirectionEntityExtensions
    {
        public static RecipeDirectionEntity MapToEntity(this RecipeDirection instruction, RecipeDirectionSectionEntity recipeDirectionSectionEntity)
        {
            if (instruction == null)
                return null;

            return new RecipeDirectionEntity()
            {
                Id = instruction.Id,
                RecipeDirectionSection = recipeDirectionSectionEntity,
                Index = instruction.Index,
                Direction = instruction.Direction,
                Note = instruction.Note
            };
        }

        public static RecipeDirection MapToDomain(this RecipeDirectionEntity entity)
        {
            if (entity == null)
                return null;

            return new RecipeDirection()
            {
                Id = entity.Id,
                Index = entity.Index,
                Direction = entity.Direction,
                Note = entity.Note
            };
        }
    }
}
