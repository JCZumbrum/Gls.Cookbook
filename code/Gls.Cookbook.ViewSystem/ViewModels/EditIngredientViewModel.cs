using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Gls.Cookbook.Domain;

namespace Gls.Cookbook.ViewSystem.ViewModels
{
    public class EditIngredientViewModel : ObservableObject, IViewModel<int>
    {
        public Task InitializeAsync(int args)
        {
            throw new System.NotImplementedException();
        }
    }
}