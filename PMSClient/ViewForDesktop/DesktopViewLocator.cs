using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using PMSClient.ViewForDesktop;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Ioc;

namespace PMSClient.ViewForDesktop
{
    public class DesktopViewLocator
    {
        public DesktopViewLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);


            SimpleIoc.Default.Register<NavigationView>(true);
            SimpleIoc.Default.Register<OrderView>();
            SimpleIoc.Default.Register<OrderEditView>();
            SimpleIoc.Default.Register<OrderCheckView>();
            SimpleIoc.Default.Register<OrderCheckEditView>();
            SimpleIoc.Default.Register<OrderSelectView>();

            SimpleIoc.Default.Register<MissonView>();

            SimpleIoc.Default.Register<PlanView>();
            SimpleIoc.Default.Register<PlanSelectView>();
            SimpleIoc.Default.Register<PlanEditView>();


            SimpleIoc.Default.Register<MaterialNeedView>();
            SimpleIoc.Default.Register<MaterialNeedEditView>();
            SimpleIoc.Default.Register<MaterialNeedSelectView>();

            SimpleIoc.Default.Register<MaterialOrderView>();
            SimpleIoc.Default.Register<MaterialOrderEditView>();
            SimpleIoc.Default.Register<MaterialOrderItemEditView>();

            SimpleIoc.Default.Register<RecordVHPView>();
            SimpleIoc.Default.Register<RecordVHPQuickEditView>();

            SimpleIoc.Default.Register<RecordTestView>();
            SimpleIoc.Default.Register<RecordTestEditView>();
            SimpleIoc.Default.Register<RecordTestSelectView>();

            SimpleIoc.Default.Register<RecordDeliveryView>();
            SimpleIoc.Default.Register<RecordDeliveryEditView>();
            SimpleIoc.Default.Register<RecordDeliveryItemEditView>();


            SimpleIoc.Default.Register<RecordMillingView>();
            SimpleIoc.Default.Register<RecordMillingEditView>();

            SimpleIoc.Default.Register<RecordMachineView>();
            SimpleIoc.Default.Register<RecordMachineEditView>();

            SimpleIoc.Default.Register<RecordDeMoldView>();
            SimpleIoc.Default.Register<RecordDeMoldEditView>();
        }

        #region NavigationProperties
        public NavigationView Navigation
        {
            get
            {
                return SimpleIoc.Default.GetInstance<NavigationView>();
            }
        }

        public OrderView Order
        {
            get
            {
                return SimpleIoc.Default.GetInstance<OrderView>();
            }
        }
        public OrderEditView OrderEdit
        {
            get
            {
                return SimpleIoc.Default.GetInstance<OrderEditView>();
            }
        }
        public OrderCheckView OrderCheck
        {
            get
            {
                return SimpleIoc.Default.GetInstance<OrderCheckView>();
            }
        }
        public OrderCheckEditView OrderCheckEdit
        {
            get { return SimpleIoc.Default.GetInstance<OrderCheckEditView>(); }
        }
        public OrderSelectView OrderSelect
        {
            get
            {
                return SimpleIoc.Default.GetInstance<OrderSelectView>();
            }
        }

        public MissonView Misson
        {
            get { return SimpleIoc.Default.GetInstance<MissonView>(); }
        }

        public PlanView Plan
        {
            get { return SimpleIoc.Default.GetInstance<PlanView>(); }
        }
        public PlanEditView PlanEdit
        {
            get { return SimpleIoc.Default.GetInstance<PlanEditView>(); }
        }
        public PlanSelectView PlanSelect
        {
            get { return SimpleIoc.Default.GetInstance<PlanSelectView>(); }
        }
        public MaterialNeedView MaterialNeed
        {
            get { return SimpleIoc.Default.GetInstance<MaterialNeedView>(); }
        }
        public MaterialNeedEditView MaterialNeedEdit
        {
            get
            {
                return SimpleIoc.Default.GetInstance<MaterialNeedEditView>();
            }
        }
        public MaterialNeedSelectView MaterialNeedSelect
        {
            get { return SimpleIoc.Default.GetInstance<MaterialNeedSelectView>(); }
        }

        public MaterialOrderView MaterialOrder
        {
            get { return SimpleIoc.Default.GetInstance<MaterialOrderView>(); }
        }
        public MaterialOrderEditView MaterialOrderEdit
        {
            get { return SimpleIoc.Default.GetInstance<MaterialOrderEditView>(); }
        }
        public MaterialOrderItemEditView MaterialOrderItemEdit
        {
            get { return SimpleIoc.Default.GetInstance<MaterialOrderItemEditView>(); }
        }

        public RecordMillingView RecordMilling
        {
            get
            {
                return SimpleIoc.Default.GetInstance<RecordMillingView>();
            }
        }
        public RecordMillingEditView RecordMillingEdit
        {
            get
            {
                return SimpleIoc.Default.GetInstance<RecordMillingEditView>();
            }
        }

        public RecordMachineView RecordMachine
        {
            get
            {
                return SimpleIoc.Default.GetInstance<RecordMachineView>();
            }
        }
        public RecordMachineEditView RecordMachineEdit
        {
            get
            {
                return SimpleIoc.Default.GetInstance<RecordMachineEditView>();
            }
        }

        public RecordDeMoldView RecordDeMold
        {
            get
            {
                return SimpleIoc.Default.GetInstance<RecordDeMoldView>();
            }
        }
        public RecordDeMoldEditView RecordDeMoldEdit
        {
            get
            {
                return SimpleIoc.Default.GetInstance<RecordDeMoldEditView>();
            }
        }

        public RecordVHPView RecordVHP
        {
            get { return SimpleIoc.Default.GetInstance<RecordVHPView>(); }
        }
        public RecordVHPQuickEditView RecordVHPQuickEdit
        {
            get { return SimpleIoc.Default.GetInstance<RecordVHPQuickEditView>(); }
        }

        public RecordTestView RecordTest
        {
            get { return SimpleIoc.Default.GetInstance<RecordTestView>(); }
        }
        public RecordTestEditView RecordTestEdit
        {
            get { return SimpleIoc.Default.GetInstance<RecordTestEditView>(); }
        }
        public RecordTestSelectView RecordTestSelect
        {
            get { return SimpleIoc.Default.GetInstance<RecordTestSelectView>(); }
        }

        public RecordDeliveryView RecordDelivery
        {
            get { return SimpleIoc.Default.GetInstance<RecordDeliveryView>(); }
        }
        public RecordDeliveryEditView RecordDeliveryEdit
        {
            get { return SimpleIoc.Default.GetInstance<RecordDeliveryEditView>(); }
        }
        public RecordDeliveryItemEditView RecordDeliveryItemEdit
        {
            get { return SimpleIoc.Default.GetInstance<RecordDeliveryItemEditView>(); }
        }
        #endregion

    }
}
