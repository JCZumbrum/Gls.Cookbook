using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gls.Cookbook.Domain.Commands.Ingredients
{
    public class CreateIngredientCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
