using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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

        private MaterialNeedView materialNeed;
        public MaterialNeedView MaterialNeed
        {
            get
            {
                if (materialNeed == null)
                {
                    materialNeed = new MaterialNeedView();
                }
                return materialNeed;
            }
        }
        private MaterialOrderView materialOrder;
        public MaterialOrderView MaterialOrder
        {
            get
            {
                if (materialOrder == null)
                {
                    materialOrder = new MaterialOrderView();
                }
                return materialOrder;
            }
        }

        private RecordTestResultView recordTestResult;
        public RecordTestResultView RecordTestResult
        {
            get
            {
                if (recordTestResult == null)
                {
                    recordTestResult = new RecordTestResultView();
                }
                return recordTestResult;
            }
        }

        private MaterialNeedEditView materialNeedEdit;
        public MaterialNeedEditView MaterialNeedEdit
        {
            get
            {
                if (materialNeedEdit == null)
                {
                    materialNeedEdit = new MaterialNeedEditView();
                }
                return materialNeedEdit;
            }
        }

        private OrderSelectView orderSelect;
        public OrderSelectView OrderSelect
        {
            get
            {
                if (orderSelect == null)
                {
                    orderSelect = new OrderSelectView();
                }
                return orderSelect;
            }
        }

        private MaterialOrderEditView materialOrderEdit;
        public MaterialOrderEditView MaterialOrderEdit
        {
            get
            {
                if (materialOrderEdit == null)
                {
                    materialOrderEdit = new MaterialOrderEditView();
                }
                return materialOrderEdit;
            }
        }

        private MaterialNeedSelectView materialNeedSelect;
        public MaterialNeedSelectView MaterialNeedSelect
        {
            get
            {
                if (materialNeedSelect == null)
                {
                    materialNeedSelect = new MaterialNeedSelectView();
                }
                return materialNeedSelect;
            }
        }




        #endregion


    }
}
