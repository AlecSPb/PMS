using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient
{
    public static class CurrentUserInformation
    {

        public static string UserName
        {
            get { return (App.Current as App).CurrentUser.UserName; }
        }

    }
}
