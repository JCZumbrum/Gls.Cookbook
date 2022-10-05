using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gls.Cookbook.DataAccess.Models
{
    public class RecipeIngredientSectionEntity
    {
        public int Id { get; set; }
        public RecipeSectionEntity RecipeSection { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public List<RecipeIngredientEntity> Ingredients { get; set; } = new List<RecipeIngredientEntity>();
    }
}
