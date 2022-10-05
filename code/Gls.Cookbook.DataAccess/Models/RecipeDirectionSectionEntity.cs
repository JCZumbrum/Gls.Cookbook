using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gls.Cookbook.DataAccess.Models
{
    public class RecipeDirectionSectionEntity
    {
        public int Id { get; set; }
        public RecipeSectionEntity RecipeSection { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public List<RecipeDirectionEntity> Directions { get; set; } = new List<RecipeDirectionEntity>();
    }
}
