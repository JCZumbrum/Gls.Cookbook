using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Gls.Cookbook.Domain;
using Gls.Cookbook.Domain.Models;
using Gls.Cookbook.Domain.Queries;
using Gls.Cookbook.ViewSystem.Args;
using static Gls.Cookbook.ViewSystem.ViewModels.MeasurementsViewModel;

namespace Gls.Cookbook.ViewSystem.ViewModels
{
    public class IngredientsViewModel : ObservableObject, IViewModel<EmptyArgs>
    {
        private IQueryIngredientService queryIngredientService;

        public class ObservableIngredient : ObservableObject
        {
            public int Id { get; set; }

            private string name;
            public string Name
            {
                get
                {
                    return name;
                }
                set
                {
                    SetProperty(ref name, value);
                }
            }
        }

        public ObservableCollection<ObservableIngredient> Ingredients { get; } = new ObservableCollection<ObservableIngredient>();

        public IngredientsViewModel(IQueryIngredientService queryIngredientService)
        {
            this.queryIngredientService = queryIngredientService;
        }

        public async Task InitializeAsync(EmptyArgs args)
        {
            List<Ingredient> ingredients = await queryIngredientService.GetAllAsync();

            Ingredients.AddRange(ingredients.OrderBy(m => m.Name).Select(m => new ObservableIngredient() { Id = m.Id, Name = m.Name }));
        }
    }
}
