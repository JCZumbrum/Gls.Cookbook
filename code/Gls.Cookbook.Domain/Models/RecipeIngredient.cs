using System.Diagnostics.Metrics;

namespace Gls.Cookbook.Domain.Models
{
    public class RecipeIngredient
    {
        public int Id { get; set; }
        public int IngredientId { get; set; }
        public decimal Quantity { get; set; }
        public int MeasurementId { get; set; }
        public string Note { get; set; }
    }
}