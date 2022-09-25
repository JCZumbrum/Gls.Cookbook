using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Gls.Cookbook.Domain;
using Gls.Cookbook.ViewSystem.Args;

namespace Gls.Cookbook.ViewSystem.ViewModels
{
    public class RecipesViewModel : ObservableObject, IViewModel<EmptyArgs>
    {
        public Task InitializeAsync(EmptyArgs args) { return Task.CompletedTask; }
    }
}
