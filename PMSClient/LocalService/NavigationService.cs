using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using PMSCommon;

namespace PMSClient
{
    public static class NavigationService
    {
        public static void GoTo(PMSViews viewToken)
        {
            Messenger.Default.Send<PMSViews>(viewToken, MainNavigationToken.Navigate);
        }
        public static void GoTo(MsgObject obj)
        {
            Messenger.Default.Send<MsgObject>(obj, MainNavigationToken.Navigate);
        }

        public static void GoTo(VToken token)
        {
            GoTo(new MsgObject() { NavigateTo = token });
        }

        public static void GoTo(VToken token, ModelObject model)
        {
            GoTo(new MsgObject() { NavigateTo = token, MsgModel = model });
        }
        public static void ShowStatusMessage(string msg = "状态信息")
        {
            Messenger.Default.Send<string>(msg, MainNavigationToken.StatusMessage);
        }

        public static void Refresh(VToken refreshtoken)
        {
            Messenger.Default.Send<MsgObject>(null, refreshtoken);
        }

        internal static void GoTo(object goBack)
        {
            throw new NotImplementedException();
        }
    }
}
