using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.Domain.Models;

namespace Gls.Cookbook.DataAccess.Models
{
    public static class IngredientEntityExtensions
    {
        public static IngredientEntity MapToEntity(this Ingredient ingredient)
        {
            return new IngredientEntity()
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Description = ingredient.Description
            };
        }
    }
}
