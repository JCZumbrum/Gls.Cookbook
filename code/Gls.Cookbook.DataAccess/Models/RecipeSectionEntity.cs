using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.Domain.Models;

namespace Gls.Cookbook.DataAccess.Models
{
    public class RecipeSectionEntity
    {
        public int Id { get; set; }
        public RecipeEntity Recipe { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public List<RecipeIngredientSectionEntity> IngredientSections { get; set; } = new List<RecipeIngredientSectionEntity>();
        public List<RecipeDirectionSectionEntity> DirectionSections { get; set; } = new List<RecipeDirectionSectionEntity>();
    }
}
