using System.Collections.Generic;

namespace Gls.Cookbook.Domain.Models
{
    public class RecipeSection
    {
        public const string MainRecipeSectionName = "Main";

        public int Id { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public List<RecipeIngredientSection> IngredientSections { get; set; } = new List<RecipeIngredientSection>();
        public List<RecipeDirectionSection> DirectionSections { get; set; } = new List<RecipeDirectionSection>();
    }
}