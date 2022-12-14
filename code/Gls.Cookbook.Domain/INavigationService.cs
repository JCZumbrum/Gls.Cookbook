using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gls.Cookbook.Domain
{
    public interface INavigationService
    {
        Task GoBackAsync();
        Task GoToAsync<TViewModel, TArgs>(TArgs args) where TViewModel : IViewModel<TArgs>;
    }
}
