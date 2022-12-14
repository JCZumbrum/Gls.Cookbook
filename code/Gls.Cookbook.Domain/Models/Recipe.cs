using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gls.Cookbook.Domain.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<RecipeNote> Notes { get; set; } = new List<RecipeNote>();
        public List<RecipeSection> Sections { get; set; } = new List<RecipeSection>();
        public List<string> Tags { get; set; } = new List<string>();
    }
}
