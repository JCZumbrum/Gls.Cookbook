using System.Collections.Generic;

namespace Gls.Cookbook.Domain.Models
{
    public class RecipeSection
    {
        public const string MainRecipeSectionName = "Main";

        public int Id { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }
        public List<RecipeIngredient> Ingredients { get; set; }
        public List<RecipeInstruction> Instructions { get; set; }
    }
}