using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Gls.Cookbook.Domain;
using Gls.Cookbook.Domain.Commands.Ingredients;
using Gls.Cookbook.Domain.Commands.Measurements;
using Gls.Cookbook.Domain.Models;
using Gls.Cookbook.ViewSystem.Args;
using Gls.Cookbook.ViewSystem.Messages;

namespace Gls.Cookbook.ViewSystem.ViewModels
{
    public class AddIngredientViewModel : ObservableObject, IViewModel<EmptyArgs>
    {
        private INavigationService navigationService;
        private ISnackBarService snackBarService;
        private ICommandService<CreateIngredientCommand, Ingredient> createIngredientService;

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

        private string note;
        public string Note
        {
            get
            {
                return note;
            }
            set
            {
                SetProperty(ref note, value);
            }
        }

        public IAsyncRelayCommand AddIngredientCommand { get; }

        public AddIngredientViewModel(INavigationService navigationService, ISnackBarService snackBarService, ICommandService<CreateIngredientCommand, Ingredient> createIngredientService)
        {
            this.navigationService = navigationService;
            this.snackBarService = snackBarService;
            this.createIngredientService = createIngredientService;

            this.AddIngredientCommand = new AsyncRelayCommand(AddIngredientAsync);
        }

        private async Task AddIngredientAsync()
        {
            Result<Ingredient> result = await createIngredientService.ExecuteAsync(new CreateIngredientCommand() { Name = this.Name, Note = this.Note });
            if (result.Success)
            {
                // notify listeners and go back
                WeakReferenceMessenger.Default.Send(new IngredientAddedMessage() { Ingredient = result.Value });
                await navigationService.GoBackAsync();
            }
            else
            {
                // toast the user with the failure
                await snackBarService.ShowAsync(result.Message);
            }
        }

        public Task InitializeAsync(EmptyArgs args) { return Task.CompletedTask; }
    }
}