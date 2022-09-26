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
            if (ingredient == null)
                return null;

            return new IngredientEntity()
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Description = ingredient.Description
            };
        }

        public static Ingredient MapToIngredient(this IngredientEntity entity)
        {
            if (entity == null)
                return null;

            return new Ingredient()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };
        }
    }
}
