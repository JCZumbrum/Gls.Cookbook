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
        public RecipeIngredientSectionEntity RecipeIngredientSection { get; set; }
        public int Index { get; set; }

        public string MinimumQuantityText { get; set; }
        public string MaximumQuantityText { get; set; }

        public int MeasurementId { get; set; }
        public MeasurementEntity Measurement { get; set; }

        public int IngredientId { get; set; }
        public IngredientEntity Ingredient { get; set; }

        public string Note { get; set; }
    }
}
