using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;

namespace PMSCommon
{
    public class NavigationService
    {
        public static void NavigateTo(string viewName)
        {
            Messenger.Default.Send<object>(viewName, NavigationToken.Navigate);
        }
    }
}
