using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSDesktopClient.View;
using PMSDesktopClient.ViewModel;

namespace PMSDesktopClient
{
    public class ViewInstance
    {
        public ViewInstance()
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

        private OrderView order;
        public OrderView Order
        {
            get
            {
                if (order == null)
                {
                    order = new OrderView();
                }
                return order;
            }
        }

        private OrderCheckView orderCheck;
        public OrderCheckView OrderCheck
        {
            get
            {
                if (orderCheck == null)
                {
                    orderCheck = new OrderCheckView();
                }
                return orderCheck;
            }
        }

        private MissonView misson;

        public MissonView Misson
        {
            get
            {
                if (misson == null)
                {
                    misson = new MissonView();
                }
                return misson;
            }
        }

        private PlanView plan;
        public PlanView Plan
        {
            get
            {
                if (plan == null)
                {
                    plan = new PlanView();
                }
                return plan;
            }
        }

        #endregion


    }
}
