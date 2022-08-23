using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gls.Cookbook.DataAccess.Models
{
    public class RecipeInstructionEntity
    {
        public int Id { get; set; }
        public RecipeSectionEntity RecipeSection { get; set; }
        public int LineNumber { get; set; }
        public string Instruction { get; set; }
        public string Note { get; set; }
    }
}
