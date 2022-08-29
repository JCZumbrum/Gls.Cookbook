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
        public static RecipeDirectionEntity MapToEntity(this RecipeDirection instruction, RecipeSectionEntity recipeSectionEntity)
        {
            return new RecipeDirectionEntity()
            {
                Id = instruction.Id,
                RecipeSection = recipeSectionEntity,
                LineNumber = instruction.LineNumber,
                Direction = instruction.Direction,
                Note = instruction.Note
            };
        }
    }
}
