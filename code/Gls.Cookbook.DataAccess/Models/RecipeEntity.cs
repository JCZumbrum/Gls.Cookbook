using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gls.Cookbook.Domain.Models;

namespace Gls.Cookbook.DataAccess.Models
{
    public class RecipeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<RecipeNoteEntity> Notes { get; set; }
        public List<RecipeSectionEntity> Sections { get; set; }
    }
}
