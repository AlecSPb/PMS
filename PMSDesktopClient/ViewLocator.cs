using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSDesktopClient.View;
using PMSDesktopClient.ViewModel;

namespace PMSDesktopClient
{
    public class ViewLocator
    {
        public ViewLocator()
        {

        }

        #region ViewProperties
        private NavigationView navigation;
        public NavigationView Navigation
        {
            get
            {
                if (navigation == null)
                {
                    navigation = new NavigationView();
                }
                return navigation;

            }
        }

        #endregion


    }
}
