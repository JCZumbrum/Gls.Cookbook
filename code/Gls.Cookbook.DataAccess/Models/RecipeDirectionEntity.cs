using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gls.Cookbook.DataAccess.Models
{
    public class RecipeDirectionEntity
    {
        public int Id { get; set; }
        public RecipeDirectionSectionEntity RecipeDirectionSection { get; set; }
        public int Index { get; set; }
        public string Direction { get; set; }
        public string Note { get; set; }
    }
}
