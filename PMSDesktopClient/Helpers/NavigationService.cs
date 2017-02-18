using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using PMSCommon;

namespace PMSDesktopClient
{
    public static class NavigationService
    {
        public static void GoTo(string viewName)
        {
            Messenger.Default.Send<string>(viewName, NavigationToken.Navigate);
        }

        public static void GoTo(MsgObject obj)
        {
            Messenger.Default.Send<MsgObject>(obj,NavigationToken.Edit);
        }
    }
}
