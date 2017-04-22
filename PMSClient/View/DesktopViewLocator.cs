using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using PMSClient.View;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Ioc;

namespace PMSClient.View
{
    public class DesktopViewLocator
    {
        public DesktopViewLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<LogInView>();

            SimpleIoc.Default.Register<NavigationView>(true);
            SimpleIoc.Default.Register<OrderView>();
            SimpleIoc.Default.Register<OrderEditView>();
            SimpleIoc.Default.Register<OrderCheckView>();
            SimpleIoc.Default.Register<OrderCheckEditView>();
            SimpleIoc.Default.Register<MissonSelectView>();

            SimpleIoc.Default.Register<MissonView>();

            SimpleIoc.Default.Register<PlanView>();
            SimpleIoc.Default.Register<PlanSearchView>();
            SimpleIoc.Default.Register<PlanSelectView>();
            SimpleIoc.Default.Register<PlanEditView>();


            SimpleIoc.Default.Register<MaterialNeedView>();
            SimpleIoc.Default.Register<MaterialNeedEditView>();
            SimpleIoc.Default.Register<MaterialNeedSelectView>();

            SimpleIoc.Default.Register<MaterialOrderView>();
            SimpleIoc.Default.Register<MaterialOrderEditView>();
            SimpleIoc.Default.Register<MaterialOrderItemEditView>();
            SimpleIoc.Default.Register<MaterialOrderItemSelectView>();

            SimpleIoc.Default.Register<MaterialInventoryInView>();
            SimpleIoc.Default.Register<MaterialInventoryInEditView>();
            SimpleIoc.Default.Register<MaterialInventoryInSelectView>();
            SimpleIoc.Default.Register<MaterialInventoryOutView>();
            SimpleIoc.Default.Register<MaterialInventoryOutEditView>();


            SimpleIoc.Default.Register<RecordVHPView>();
            SimpleIoc.Default.Register<RecordVHPQuickEditView>();

            SimpleIoc.Default.Register<RecordTestView>();
            SimpleIoc.Default.Register<RecordTestEditView>();
            SimpleIoc.Default.Register<RecordTestSelectView>();
            SimpleIoc.Default.Register<RecordTestDocView>();

            SimpleIoc.Default.Register<RecordBondingView>();
            SimpleIoc.Default.Register<RecordBondingEditView>();

            SimpleIoc.Default.Register<ProductView>();
            SimpleIoc.Default.Register<ProductEditView>();
            SimpleIoc.Default.Register<ProductSelectView>();


            SimpleIoc.Default.Register<DeliveryView>();
            SimpleIoc.Default.Register<DeliveryEditView>();
            SimpleIoc.Default.Register<DeliveryItemEditView>();
            SimpleIoc.Default.Register<DeliveryItemListView>();

            SimpleIoc.Default.Register<RecordMillingView>();
            SimpleIoc.Default.Register<RecordMillingEditView>();

            SimpleIoc.Default.Register<RecordMachineView>();
            SimpleIoc.Default.Register<RecordMachineEditView>();
            SimpleIoc.Default.Register<RecordMachineSelectView>();


            SimpleIoc.Default.Register<RecordDeMoldView>();
            SimpleIoc.Default.Register<RecordDeMoldEditView>();
        }

        #region NavigationProperties
        public LogInView LogIn
        {
            get { return SimpleIoc.Default.GetInstance<LogInView>(); }
        }

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
        public MissonSelectView MissonSelect
        {
            get
            {
                return SimpleIoc.Default.GetInstance<MissonSelectView>();
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
        public PlanSearchView PlanSearch
        {
            get { return SimpleIoc.Default.GetInstance<PlanSearchView>(); }
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
        public MaterialOrderItemSelectView MaterialOrderItemSelect
        {
            get { return SimpleIoc.Default.GetInstance<MaterialOrderItemSelectView>(); }
        }
        public MaterialOrderItemEditView MaterialOrderItemEdit
        {
            get { return SimpleIoc.Default.GetInstance<MaterialOrderItemEditView>(); }
        }
        public MaterialInventoryInView MaterialInventoryIn
        {
            get { return SimpleIoc.Default.GetInstance<MaterialInventoryInView>(); }
        }
        public MaterialInventoryInEditView MaterialInventoryInEdit
        {
            get { return SimpleIoc.Default.GetInstance<MaterialInventoryInEditView>(); }
        }
        public MaterialInventoryInSelectView MaterialInventoryInSelect
        {
            get { return SimpleIoc.Default.GetInstance<MaterialInventoryInSelectView>(); }
        }
        public MaterialInventoryOutView MaterialInventoryOut
        {
            get { return SimpleIoc.Default.GetInstance<MaterialInventoryOutView>(); }
        }
        public MaterialInventoryOutEditView MaterialInventoryOutEdit
        {
            get { return SimpleIoc.Default.GetInstance<MaterialInventoryOutEditView>(); }

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
        public RecordMachineSelectView RecordMachineSelect
        {
            get
            {
                return SimpleIoc.Default.GetInstance<RecordMachineSelectView>();
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
        public RecordTestDocView RecordTestDoc
        {
            get { return SimpleIoc.Default.GetInstance<RecordTestDocView>(); }
        }
        public RecordBondingView RecordBonding
        {
            get { return SimpleIoc.Default.GetInstance<RecordBondingView>(); }
        }
        public RecordBondingEditView RecordBondingEdit
        {
            get { return SimpleIoc.Default.GetInstance<RecordBondingEditView>(); }
        }


        public ProductView Product
        {
            get { return SimpleIoc.Default.GetInstance<ProductView>(); }
        }
        public ProductEditView ProductEdit
        {
            get { return SimpleIoc.Default.GetInstance<ProductEditView>(); }
        }
        public ProductSelectView ProductSelect
        {
            get { return SimpleIoc.Default.GetInstance<ProductSelectView>(); }
        }


        public DeliveryView Delivery
        {
            get { return SimpleIoc.Default.GetInstance<DeliveryView>(); }
        }
        public DeliveryEditView DeliveryEdit
        {
            get { return SimpleIoc.Default.GetInstance<DeliveryEditView>(); }
        }
        public DeliveryItemEditView DeliveryItemEdit
        {
            get { return SimpleIoc.Default.GetInstance<DeliveryItemEditView>(); }
        }
        public DeliveryItemListView DeliveryItemList
        {
            get { return SimpleIoc.Default.GetInstance<DeliveryItemListView>(); }
        }
        #endregion

    }
}
