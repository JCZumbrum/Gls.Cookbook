using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.Domain.Models;

namespace Gls.Cookbook.DataAccess.Models
{
    public class RecipeIngredientEntity
    {
        public int Id { get; set; }
        public RecipeEntity Recipe { get; set; }
        public IngredientEntity Ingredient { get; set; }
        public decimal Quantity { get; set; }
        public MeasurementEntity Measurement { get; set; }
        public string Note { get; set; }
    }
}
