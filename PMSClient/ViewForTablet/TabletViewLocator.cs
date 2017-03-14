using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using PMSClient.ViewForTablet;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Ioc;

namespace PMSClient.ViewForTablet
{
    public class TabletViewLocator
    {
        public TabletViewLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<NavigationView>(true);
            SimpleIoc.Default.Register<RecordVHPView>();
            SimpleIoc.Default.Register<RecordVHPQuickEditView>();

            //SimpleIoc.Default.Register<PlanSelectView>();
            SimpleIoc.Default.Register<RecordTestView>();
            SimpleIoc.Default.Register<RecordTestSelectView>();
            SimpleIoc.Default.Register<RecordTestEditView>();


        }
        #region Properties
        public RecordVHPView RecordVHP
        {
            get { return SimpleIoc.Default.GetInstance<RecordVHPView>(); }
        }
        public RecordVHPQuickEditView RecordVHPQuickEdit
        {
            get { return SimpleIoc.Default.GetInstance<RecordVHPQuickEditView>(); }
        }

        //public PlanSelectView PlanSelect
        //{
        //    get { return SimpleIoc.Default.GetInstance<PlanSelectView>(); }
        //}
        #endregion

        #region ViewProperties
        public NavigationView Navigation
        {
            get
            {
                return SimpleIoc.Default.GetInstance<NavigationView>();
            }
        }

        //private OrderView order;
        //public OrderView Order
        //{
        //    get
        //    {
        //        if (order == null)
        //        {
        //            order = new OrderView();
        //        }
        //        return order;
        //    }
        //}
        //private OrderEditView orderEdit;
        //public OrderEditView OrderEdit
        //{
        //    get
        //    {
        //        if (orderEdit == null)
        //        {
        //            orderEdit = new OrderEditView();
        //        }
        //        return orderEdit;
        //    }
        //}
        //private OrderCheckView orderCheck;
        //public OrderCheckView OrderCheck
        //{
        //    get
        //    {
        //        if (orderCheck == null)
        //        {
        //            orderCheck = new OrderCheckView();
        //        }
        //        return orderCheck;
        //    }
        //}

        //private MissonView misson;

        //public MissonView Misson
        //{
        //    get
        //    {
        //        if (misson == null)
        //        {
        //            misson = new MissonView();
        //        }
        //        return misson;
        //    }
        //}

        //private PlanView plan;
        //public PlanView Plan
        //{
        //    get
        //    {
        //        if (plan == null)
        //        {
        //            plan = new PlanView();
        //        }
        //        return plan;
        //    }
        //}

        //private MaterialNeedView materialNeed;
        //public MaterialNeedView MaterialNeed
        //{
        //    get
        //    {
        //        if (materialNeed == null)
        //        {
        //            materialNeed = new MaterialNeedView();
        //        }
        //        return materialNeed;
        //    }
        //}
        //private MaterialOrderView materialOrder;
        //public MaterialOrderView MaterialOrder
        //{
        //    get
        //    {
        //        if (materialOrder == null)
        //        {
        //            materialOrder = new MaterialOrderView();
        //        }
        //        return materialOrder;
        //    }
        //}


        //private MaterialNeedEditView materialNeedEdit;
        //public MaterialNeedEditView MaterialNeedEdit
        //{
        //    get
        //    {
        //        if (materialNeedEdit == null)
        //        {
        //            materialNeedEdit = new MaterialNeedEditView();
        //        }
        //        return materialNeedEdit;
        //    }
        //}

        //private OrderSelectView orderSelect;
        //public OrderSelectView OrderSelect
        //{
        //    get
        //    {
        //        if (orderSelect == null)
        //        {
        //            orderSelect = new OrderSelectView();
        //        }
        //        return orderSelect;
        //    }
        //}

        //private MaterialOrderEditView materialOrderEdit;
        //public MaterialOrderEditView MaterialOrderEdit
        //{
        //    get
        //    {
        //        if (materialOrderEdit == null)
        //        {
        //            materialOrderEdit = new MaterialOrderEditView();
        //        }
        //        return materialOrderEdit;
        //    }
        //}

        //private MaterialNeedSelectView materialNeedSelect;
        //public MaterialNeedSelectView MaterialNeedSelect
        //{
        //    get
        //    {
        //        if (materialNeedSelect == null)
        //        {
        //            materialNeedSelect = new MaterialNeedSelectView();
        //        }
        //        return materialNeedSelect;
        //    }
        //}

        //private RecordTestView recordTest;
        //public RecordTestView RecordTest
        //{
        //    get
        //    {
        //        return SimpleIoc.Default.GetInstance<RecordTestView>();
        //    }
        //}

        //private RecordTestEditView recordTestEdit;
        //public RecordTestEditView RecordTestEdit
        //{
        //    get
        //    {
        //        return SimpleIoc.Default.GetInstance<RecordTestEditView>();
        //    }
        //}

        //private RecordTestSelectView recordTestSelect;
        //public RecordTestSelectView RecordTestSelect
        //{
        //    get
        //    {
        //        return SimpleIoc.Default.GetInstance<RecordTestSelectView>();
        //    }
        //}

        //private MaterialOrderItemEditView materialOrderItemEdit;
        //public MaterialOrderItemEditView MaterialOrderItemEdit
        //{
        //    get
        //    {
        //        if (materialOrderItemEdit == null)
        //        {
        //            materialOrderItemEdit = new MaterialOrderItemEditView();
        //        }
        //        return materialOrderItemEdit;
        //    }
        //}


        //private RecordDeliveryView recordDelivery;
        //public RecordDeliveryView RecordDelivery
        //{
        //    get
        //    {
        //        if (recordDelivery == null)
        //        {
        //            recordDelivery = new RecordDeliveryView();
        //        }
        //        return recordDelivery;
        //    }
        //}

        //private RecordDeliveryEditView recordDeliveryEdit;
        //public RecordDeliveryEditView RecordDeliveryEdit
        //{
        //    get
        //    {
        //        if (recordDeliveryEdit == null)
        //        {
        //            recordDeliveryEdit = new RecordDeliveryEditView();
        //        }
        //        return recordDeliveryEdit;
        //    }
        //}

        //private RecordDeliveryItemEditView recordDeliveryItemEdit;
        //public RecordDeliveryItemEditView RecordDeliveryItemEdit
        //{
        //    get
        //    {
        //        if (recordDeliveryItemEdit == null)
        //        {
        //            recordDeliveryItemEdit = new RecordDeliveryItemEditView();
        //        }
        //        return recordDeliveryItemEdit;
        //    }
        //}
        #endregion


    }
}
