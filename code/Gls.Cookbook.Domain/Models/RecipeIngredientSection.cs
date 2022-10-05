﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gls.Cookbook.Domain.Models
{
    public class RecipeIngredientSection
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public List<RecipeIngredient> Ingredients { get; set; } = new List<RecipeIngredient>();
    }
}
