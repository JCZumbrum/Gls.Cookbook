using System.Diagnostics.Metrics;

namespace Gls.Cookbook.Domain.Models
{
    public class RecipeIngredient
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string MinimumQuantityText { get; set; }
        public string MaximumQuantityText { get; set; }
        public int MeasurementId { get; set; }
        public int IngredientId { get; set; }
        public string Note { get; set; }
    }
}