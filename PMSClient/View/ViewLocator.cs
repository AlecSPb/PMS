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
    public class ViewLocator
    {
        public ViewLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<LogInView>(true);

            SimpleIoc.Default.Register<NavigationView>(true);
            SimpleIoc.Default.Register<NavigationWorkFlowView>(true);

            SimpleIoc.Default.Register<OrderView>();
            SimpleIoc.Default.Register<OrderEditView>();
            SimpleIoc.Default.Register<OrderUnCompletedView>();
            SimpleIoc.Default.Register<OrderCheckView>();
            SimpleIoc.Default.Register<OrderCheckEditView>();

            SimpleIoc.Default.Register<OutSourceView>();
            SimpleIoc.Default.Register<OutSourceEditView>();

            SimpleIoc.Default.Register<MissonSelectView>();
            SimpleIoc.Default.Register<MissonView>();
            SimpleIoc.Default.Register<MissonUnCompletedView>();

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
            SimpleIoc.Default.Register<MaterialOrderItemListView>();
            SimpleIoc.Default.Register<MaterialOrderItemListFlagView>();

            SimpleIoc.Default.Register<MaterialInventoryInView>();
            SimpleIoc.Default.Register<MaterialInventoryInEditView>();
            SimpleIoc.Default.Register<MaterialInventoryInSelectView>();
            SimpleIoc.Default.Register<MaterialInventoryInUnCompletedView>();
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
            SimpleIoc.Default.Register<RecordBondingSelectView>();


            SimpleIoc.Default.Register<ProductView>();
            SimpleIoc.Default.Register<ProductEditView>();
            SimpleIoc.Default.Register<ProductSelectView>();
            SimpleIoc.Default.Register<ProductUnCompletedView>();

            SimpleIoc.Default.Register<PlateView>();
            SimpleIoc.Default.Register<PlateEditView>();
            SimpleIoc.Default.Register<PlateSelectView>();
            SimpleIoc.Default.Register<PlateUnCompletedView>();

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
            SimpleIoc.Default.Register<RecordDeMoldSelectView>();

            SimpleIoc.Default.Register<CustomerView>();
            SimpleIoc.Default.Register<CustomerEditView>();

            SimpleIoc.Default.Register<OrderStatisticView>();
            SimpleIoc.Default.Register<PlanStatisticView>();
            SimpleIoc.Default.Register<DeliveryStatisticView>();
            SimpleIoc.Default.Register<ProductStatisticView>();

            SimpleIoc.Default.Register<FeedBackView>();
            SimpleIoc.Default.Register<FeedBackEditView>();

            SimpleIoc.Default.Register<CheckListView>();
            SimpleIoc.Default.Register<CheckListEditView>();
            SimpleIoc.Default.Register<CheckListReadView>();

            SimpleIoc.Default.Register<OutputView>();

            SimpleIoc.Default.Register<DebugView>();
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
        public NavigationWorkFlowView NavigationWorkFlow
        {
            get
            {
                return SimpleIoc.Default.GetInstance<NavigationWorkFlowView>();
            }
        }
        public OrderView Order
        {
            get
            {
                return SimpleIoc.Default.GetInstance<OrderView>();
            }
        }
        public OrderUnCompletedView OrderUnCompleted
        {
            get
            {
                return SimpleIoc.Default.GetInstance<OrderUnCompletedView>();
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

        public OutSourceView OutSource
        {
            get
            {
                return SimpleIoc.Default.GetInstance<OutSourceView>();
            }
        }
        public OutSourceEditView OutSourceEdit
        {
            get
            {
                return SimpleIoc.Default.GetInstance<OutSourceEditView>();
            }
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
        public MissonUnCompletedView MissonUnCompleted
        {
            get { return SimpleIoc.Default.GetInstance<MissonUnCompletedView>(); }
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
        public MaterialOrderItemListView MaterialOrderItemList
        {
            get { return SimpleIoc.Default.GetInstance<MaterialOrderItemListView>(); }
        }
        public MaterialOrderItemListFlagView MaterialOrderItemListFlag
        {
            get { return SimpleIoc.Default.GetInstance<MaterialOrderItemListFlagView>(); }
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
        public MaterialInventoryInUnCompletedView MaterialInventoryInUnCompleted
        {
            get { return SimpleIoc.Default.GetInstance<MaterialInventoryInUnCompletedView>(); }
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
        public RecordDeMoldSelectView RecordDeMoldSelect
        {
            get
            {
                return SimpleIoc.Default.GetInstance<RecordDeMoldSelectView>();
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
        public RecordBondingSelectView RecordBondingSelect
        {
            get { return SimpleIoc.Default.GetInstance<RecordBondingSelectView>(); }
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
        public ProductUnCompletedView ProductUnCompleted
        {
            get { return SimpleIoc.Default.GetInstance<ProductUnCompletedView>(); }
        }


        public PlateView Plate
        {
            get { return SimpleIoc.Default.GetInstance<PlateView>(); }
        }
        public PlateEditView PlateEdit
        {
            get { return SimpleIoc.Default.GetInstance<PlateEditView>(); }
        }
        public PlateSelectView PlateSelect
        {
            get { return SimpleIoc.Default.GetInstance<PlateSelectView>(); }
        }
        public PlateUnCompletedView PlateUnCompleted
        {
            get { return SimpleIoc.Default.GetInstance<PlateUnCompletedView>(); }
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

        public CustomerView Customer
        {
            get { return SimpleIoc.Default.GetInstance<CustomerView>(); }
        }
        public CustomerEditView CustomerEdit
        {
            get { return SimpleIoc.Default.GetInstance<CustomerEditView>(); }
        }

        public OrderStatisticView OrderStatistic
        {
            get { return SimpleIoc.Default.GetInstance<OrderStatisticView>(); }
        }

        public PlanStatisticView PlanStatistic
        {
            get { return SimpleIoc.Default.GetInstance<PlanStatisticView>(); }
        }
        public ProductStatisticView ProductStatistic
        {
            get { return SimpleIoc.Default.GetInstance<ProductStatisticView>(); }
        }
        public DeliveryStatisticView DeliveryStatistic
        {
            get { return SimpleIoc.Default.GetInstance<DeliveryStatisticView>(); }
        }

        public FeedBackView FeedBack
        {
            get { return SimpleIoc.Default.GetInstance<FeedBackView>(); }
        }
        public FeedBackEditView FeedBackEdit
        {
            get { return SimpleIoc.Default.GetInstance<FeedBackEditView>(); }
        }

        public CheckListView CheckList
        {
            get { return SimpleIoc.Default.GetInstance<CheckListView>(); }
        }
        public CheckListEditView CheckListEdit
        {
            get { return SimpleIoc.Default.GetInstance<CheckListEditView>(); }
        }
        public CheckListReadView CheckListtRead
        {
            get { return SimpleIoc.Default.GetInstance<CheckListReadView>(); }
        }
        public OutputView Output
        {
            get { return SimpleIoc.Default.GetInstance<OutputView>(); }
        }
        public DebugView Debug
        {
            get { return SimpleIoc.Default.GetInstance<DebugView>(); }
        }
        #endregion

    }
}
