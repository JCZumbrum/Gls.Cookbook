using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Gls.Cookbook.Domain;
using Gls.Cookbook.Domain.Commands.Ingredients;
using Gls.Cookbook.Domain.Models;
using Gls.Cookbook.Domain.Queries;
using Gls.Cookbook.ViewSystem.Messages;

namespace Gls.Cookbook.ViewSystem.ViewModels
{
    public class EditIngredientViewModel : ObservableObject, IViewModel<int>
    {
        private INavigationService navigationService;
        private ISnackBarService snackBarService;
        private IQueryIngredientService queryIngredientService;
        private ICommandService<DeleteIngredientCommand> deleteIngredientService;
        private ICommandService<UpdateIngredientCommand, Ingredient> updateIngredientService;
        private int ingredientId;

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

        public IAsyncRelayCommand DeleteMeasurementCommand { get; }
        public IAsyncRelayCommand UpdateMeasurementCommand { get; }

        public EditIngredientViewModel(INavigationService navigationService, ISnackBarService snackBarService, IQueryIngredientService queryIngredientService, ICommandService<UpdateIngredientCommand, Ingredient> updateIngredientService, ICommandService<DeleteIngredientCommand> deleteIngredientService)
        {
            this.navigationService = navigationService;
            this.snackBarService = snackBarService;
            this.queryIngredientService = queryIngredientService;
            this.deleteIngredientService = deleteIngredientService;
            this.updateIngredientService = updateIngredientService;

            this.DeleteMeasurementCommand = new AsyncRelayCommand(DeleteMeasurement);
            this.UpdateMeasurementCommand = new AsyncRelayCommand(UpdateMeasurement);
        }

        private async Task DeleteMeasurement()
        {
            Result result = await deleteIngredientService.ExecuteAsync(new DeleteIngredientCommand() { Id = ingredientId });
            if (result.Success)
            {
                // notify listeners and go back
                WeakReferenceMessenger.Default.Send(new IngredientDeletedMessage() { IngredientId = this.ingredientId });
                await navigationService.GoBackAsync();
            }
            else
            {
                // toast the user with the failure
                await snackBarService.ShowAsync(result.Message);
            }
        }

        private async Task UpdateMeasurement()
        {
            Result<Ingredient> result = await updateIngredientService.ExecuteAsync(new UpdateIngredientCommand() { Id = this.ingredientId, Name = this.Name, Note = this.Note });

            if (result.Success)
            {
                // notify listeners and go back
                WeakReferenceMessenger.Default.Send(new IngredientUpdatedMessage() { Ingredient = result.Value });
                await navigationService.GoBackAsync();
            }
            else
            {
                // toast the user with the failure
                await snackBarService.ShowAsync(result.Message);
            }
        }

        public async Task InitializeAsync(int ingredientId)
        {
            Ingredient ingredient = await queryIngredientService.GetByIdAsync(ingredientId);

            this.ingredientId = ingredient.Id;
            this.Name = ingredient.Name;
            this.Note = ingredient.Note;
        }
    }
}