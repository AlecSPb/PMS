using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using PMSCommon;

namespace PMSTabletClient
{
    public static class NavigationService
    {
        public static void NavigateTo(string viewName)
        {
            Messenger.Default.Send<string>(viewName, NavigationToken.Navigate);
        }
    }
}
