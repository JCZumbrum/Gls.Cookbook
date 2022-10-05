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
    }
}
