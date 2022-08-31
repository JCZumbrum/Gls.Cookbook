using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gls.Cookbook.DataAccess.Models
{
    public class RecipeNoteEntity
    {
        public int Id { get; set; }
        public RecipeEntity Recipe { get; set; }
        public int Index { get; set; }
        public string Note { get; set; }
    }
}
