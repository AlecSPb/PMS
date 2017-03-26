using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.UserService;

namespace PMSClient
{
    public static class CurrentUserInformation
    {

        public static string UserName
        {
            get { return (App.Current as App).CurrentUser.UserName; }
        }

        public static void SetCurrentUser(DcUser user)
        {
            var currentUser = (App.Current as App).CurrentUser;
            currentUser = user;
        }
    }
}
