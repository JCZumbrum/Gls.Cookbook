using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Gls.Cookbook.Domain;
using Gls.Cookbook.ViewSystem.Args;

namespace Gls.Cookbook.ViewSystem.ViewModels
{
    public class AddIngredientViewModel : ObservableObject, IViewModel<EmptyArgs>
    {
        public Task InitializeAsync(EmptyArgs args)
        {
            throw new System.NotImplementedException();
        }
    }
}