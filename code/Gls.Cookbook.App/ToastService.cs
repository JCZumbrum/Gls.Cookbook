using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using Gls.Cookbook.Domain;

namespace Gls.Cookbook.App
{
    public class ToastService : IToastService
    {
        public void Make(string message)
        {
            Toast.Make(message);
        }
    }
}
