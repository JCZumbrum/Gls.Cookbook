using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.Domain.Models;

namespace Gls.Cookbook.DataAccess.Models
{
    public static class RecipeInstructionEntityExtensions
    {
        public static RecipeInstructionEntity MapToEntity(this RecipeInstruction instruction, RecipeSectionEntity recipeSectionEntity)
        {
            return new RecipeInstructionEntity()
            {
                Id = instruction.Id,
                RecipeSection = recipeSectionEntity,
                LineNumber = instruction.LineNumber,
                Instruction = instruction.Instruction,
                Note = instruction.Note
            };
        }
    }
}
