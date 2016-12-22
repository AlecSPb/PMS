using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;

namespace PMSTabletClient
{
    public static class NavigationWizard
    {
        public static void GoToMainNavigation()
        {
            Messenger.Default.Send<string>(null, ViewToken.MainNavigate);
        }
        public static void GoToRecordVHP()
        {
            Messenger.Default.Send<string>(null, ViewToken.RecordVHP);
        }

        internal static void GoToVHPRecordEdit()
        {
            Messenger.Default.Send<string>(null, ViewToken.RecordVHPEdit);
        }
    }
}
