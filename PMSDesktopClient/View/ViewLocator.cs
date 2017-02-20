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
        private OrderEditView orderEdit;
        public OrderEditView OrderEdit
        {
            get
            {
                if (orderEdit == null)
                {
                    orderEdit = new OrderEditView();
                }
                return orderEdit;
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

        private PlanSelectView planSelect;
        public PlanSelectView PlanSelect
        {
            get
            {
                if (planSelect == null)
                {
                    planSelect = new PlanSelectView();
                }
                return planSelect;
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

        private RecordTestResultEditView recordTestResultEdit;
        public RecordTestResultEditView RecordTestResultEdit
        {
            get
            {
                if (recordTestResultEdit == null)
                {
                    recordTestResultEdit = new RecordTestResultEditView();
                }
                return recordTestResultEdit;
            }
        }

        private RecordTestResultSelectView recordTestResultSelect;
        public RecordTestResultSelectView RecordTestResultSelect
        {
            get
            {
                if (recordTestResultSelect == null)
                {
                    recordTestResultSelect = new RecordTestResultSelectView();
                }
                return recordTestResultSelect;
            }
        }

        private MaterialOrderItemEditView materialOrderItemEdit;
        public MaterialOrderItemEditView MaterialOrderItemEdit
        {
            get
            {
                if (materialOrderItemEdit == null)
                {
                    materialOrderItemEdit = new MaterialOrderItemEditView();
                }
                return materialOrderItemEdit;
            }
        }


        private RecordDeliveryView recordDelivery;
        public RecordDeliveryView RecordDelivery
        {
            get
            {
                if (recordDelivery == null)
                {
                    recordDelivery = new RecordDeliveryView();
                }
                return recordDelivery;
            }
        }

        private RecordDeliveryEditView recordDeliveryEdit;
        public RecordDeliveryEditView RecordDeliveryEdit
        {
            get
            {
                if (recordDeliveryEdit == null)
                {
                    recordDeliveryEdit = new RecordDeliveryEditView();
                }
                return recordDeliveryEdit;
            }
        }

        private RecordDeliveryItemEditView recordDeliveryItemEdit;
        public RecordDeliveryItemEditView RecordDeliveryItemEdit
        {
            get
            {
                if (recordDeliveryItemEdit == null)
                {
                    recordDeliveryItemEdit = new RecordDeliveryItemEditView();
                }
                return recordDeliveryItemEdit;
            }
        }
        #endregion


    }
}
