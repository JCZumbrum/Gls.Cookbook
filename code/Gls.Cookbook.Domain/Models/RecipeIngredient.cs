using System.Diagnostics.Metrics;

namespace Gls.Cookbook.Domain.Models
{
    public class RecipeIngredient
    {
        public int Id { get; set; }
        public Ingredient Ingredient { get; set; }
        public decimal Quantity { get; set; }
        public Measurement Measurement { get; set; }
        public string Note { get; set; }
    }
}