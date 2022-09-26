using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Gls.Cookbook.Domain;

namespace Gls.Cookbook.App
{
    public class SnackBarService : ISnackBarService
    {
        public async Task ShowAsync(string message)
        {
            using (ISnackbar snackbar = Snackbar.Make(message))
            {
                await snackbar.Show();
            }
        }
    }
}
