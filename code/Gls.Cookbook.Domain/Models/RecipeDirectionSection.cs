using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gls.Cookbook.Domain.Models
{
    public class RecipeDirectionSection
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public List<RecipeDirection> Directions { get; set; } = new List<RecipeDirection>();
    }
}
