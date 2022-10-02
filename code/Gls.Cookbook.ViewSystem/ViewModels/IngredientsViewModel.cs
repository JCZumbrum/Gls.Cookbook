using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gls.Cookbook.Domain;
using Gls.Cookbook.Domain.Models;
using Gls.Cookbook.Domain.Queries;
using Gls.Cookbook.ViewSystem.Args;
using static Gls.Cookbook.ViewSystem.ViewModels.MeasurementsViewModel;

namespace Gls.Cookbook.ViewSystem.ViewModels
{
    public class IngredientsViewModel : ObservableRecipient, IViewModel<EmptyArgs>
    {
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

        private INavigationService navigationService;
        private IQueryIngredientService queryIngredientService;

        public ObservableCollection<ObservableIngredient> Ingredients { get; } = new ObservableCollection<ObservableIngredient>();

        public IAsyncRelayCommand<ObservableMeasurement> IngredientSelectedCommand { get; }
        public IAsyncRelayCommand AddIngredientCommand { get; }
        public IRelayCommand LoadedCommand { get; }
        public IRelayCommand UnloadedCommand { get; }

        public IngredientsViewModel(INavigationService navigationService, IQueryIngredientService queryIngredientService)
        {
            this.navigationService = navigationService;
            this.queryIngredientService = queryIngredientService;

            this.IngredientSelectedCommand = new AsyncRelayCommand<ObservableMeasurement>(ViewIngredient);
            this.AddIngredientCommand = new AsyncRelayCommand(AddIngredient);
            this.LoadedCommand = new RelayCommand(Load);
            this.UnloadedCommand = new RelayCommand(Unload);
        }

        private void Load()
        {
            this.IsActive = true;
        }

        private void Unload()
        {
            this.IsActive = false;
        }

        private async Task ViewIngredient(ObservableMeasurement arg)
        {
            await navigationService.GoToAsync<EditIngredientViewModel, int>(arg.Id);
        }

        private async Task AddIngredient()
        {
            await navigationService.GoToAsync<AddIngredientViewModel, EmptyArgs>(new EmptyArgs());
        }

        public async Task InitializeAsync(EmptyArgs args)
        {
            List<Ingredient> ingredients = await queryIngredientService.GetAllAsync();

            Ingredients.AddRange(ingredients.OrderBy(m => m.Name).Select(m => new ObservableIngredient() { Id = m.Id, Name = m.Name }));
        }
    }
}
