using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gls.Cookbook.Domain
{
    public interface ISnackBarService
    {
        Task ShowAsync(string message);
    }
}
