using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Gls.Cookbook.Domain;
using Gls.Cookbook.Domain.Models;
using Gls.Cookbook.Domain.Queries;
using Gls.Cookbook.ViewSystem.Args;
using Gls.Cookbook.ViewSystem.Messages;

namespace Gls.Cookbook.ViewSystem.ViewModels
{
    public class IngredientsViewModel :
        ObservableRecipient,
        IViewModel<EmptyArgs>,
        IRecipient<IngredientAddedMessage>,
        IRecipient<IngredientUpdatedMessage>,
        IRecipient<IngredientDeletedMessage>
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

        private ObservableIngredient selectedIngredient;
        public ObservableIngredient SelectedIngredient
        {
            get
            {
                return selectedIngredient;
            }
            set
            {
                SetProperty(ref selectedIngredient, value);
            }
        }

        public ObservableCollection<ObservableIngredient> Ingredients { get; } = new ObservableCollection<ObservableIngredient>();

        public IAsyncRelayCommand IngredientSelectedCommand { get; }
        public IAsyncRelayCommand AddIngredientCommand { get; }
        public IAsyncRelayCommand LoadedCommand { get; }
        public IRelayCommand UnloadedCommand { get; }

        public IngredientsViewModel(INavigationService navigationService, IQueryIngredientService queryIngredientService)
        {
            this.navigationService = navigationService;
            this.queryIngredientService = queryIngredientService;

            this.IngredientSelectedCommand = new AsyncRelayCommand(ViewIngredient);
            this.AddIngredientCommand = new AsyncRelayCommand(AddIngredient);
            this.LoadedCommand = new AsyncRelayCommand(LoadAsync);
            this.UnloadedCommand = new RelayCommand(Unload);
        }

        private async Task LoadAsync()
        {
            List<Ingredient> ingredients = await queryIngredientService.GetAllAsync();

            Ingredients.AddRange(ingredients.OrderBy(m => m.Name).Select(m => new ObservableIngredient() { Id = m.Id, Name = m.Name }));

            this.IsActive = true;
        }

        private void Unload()
        {
            this.IsActive = false;
        }

        private async Task ViewIngredient()
        {
            if (SelectedIngredient == null)
                return;

            int ingredientId = SelectedIngredient.Id;

            await navigationService.GoToAsync<EditIngredientViewModel, int>(ingredientId);

            SelectedIngredient = null;
        }

        private async Task AddIngredient()
        {
            await navigationService.GoToAsync<AddIngredientViewModel, EmptyArgs>(new EmptyArgs());
        }

        public Task InitializeAsync(EmptyArgs args) { return Task.CompletedTask; }

        public void Receive(IngredientAddedMessage message)
        {
            Ingredient ingredient = message.Ingredient;

            Ingredients.Add(new ObservableIngredient() { Id = ingredient.Id, Name = ingredient.Name });
        }

        public void Receive(IngredientUpdatedMessage message)
        {
            Ingredient ingredient = message.Ingredient;

            ObservableIngredient existingMeasurement = Ingredients.FirstOrDefault(m => m.Id == ingredient.Id);
            if (existingMeasurement != null)
            {
                existingMeasurement.Name = ingredient.Name;
            }
        }

        public void Receive(IngredientDeletedMessage message)
        {
            ObservableIngredient ingredient = Ingredients.FirstOrDefault(m => m.Id == message.IngredientId);
            if (ingredient != null)
                Ingredients.Remove(ingredient);
        }
    }
}
