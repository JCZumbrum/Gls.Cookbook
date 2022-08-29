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
        public int Order { get; set; }
        public string Name { get; set; }
        public List<RecipeIngredientEntity> Ingredients { get; set; } = new List<RecipeIngredientEntity>();
        public List<RecipeDirectionEntity> Directions { get; set; } = new List<RecipeDirectionEntity>();
    }
}
