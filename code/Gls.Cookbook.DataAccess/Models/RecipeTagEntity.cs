using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Gls.Cookbook.DataAccess.Models
{
    [Keyless]
    public class RecipeTagEntity
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
