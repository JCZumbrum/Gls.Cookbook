using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gls.Cookbook.Domain
{
    public interface INavigationService
    {
        Task GoToAsync<T>() where T : IViewModel;
        Task GoToAsync<T>(IDictionary<string, object> parameters) where T : IViewModel;
    }
}
