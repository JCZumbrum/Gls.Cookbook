using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Gls.Cookbook.Domain;

namespace Gls.Cookbook.ViewSystem.ViewModels
{
    public class CreateRecipeViewModel : ObservableObject, IViewModel<EmptyArgs>
    {
        public Task InitializeAsync(EmptyArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
