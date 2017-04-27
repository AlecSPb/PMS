/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:PMSClient"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace PMSClient.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<NavigationVM>(true);

            SimpleIoc.Default.Register<OrderVM>();
            SimpleIoc.Default.Register<OrderEditVM>();

            SimpleIoc.Default.Register<OrderCheckVM>();
            SimpleIoc.Default.Register<OrderCheckEditVM>();

            SimpleIoc.Default.Register<MissonVM>();
            SimpleIoc.Default.Register<MissonSelectVM>();

            SimpleIoc.Default.Register<PlanVM>();
            SimpleIoc.Default.Register<PlanSearchVM>();
            SimpleIoc.Default.Register<PlanEditVM>();
            SimpleIoc.Default.Register<PlanSelectVM>();


            SimpleIoc.Default.Register<MaterialNeedVM>();
            SimpleIoc.Default.Register<MaterialNeedEditVM>();
            SimpleIoc.Default.Register<MaterialNeedSelectVM>();

            SimpleIoc.Default.Register<MaterialOrderVM>();
            SimpleIoc.Default.Register<MaterialOrderEditVM>();
            SimpleIoc.Default.Register<MaterialOrderItemEditVM>();
            SimpleIoc.Default.Register<MaterialOrderItemSelectVM>();


            SimpleIoc.Default.Register<MaterialInventoryInVM>();
            SimpleIoc.Default.Register<MaterialInventoryInEditVM>();
            SimpleIoc.Default.Register<MaterialInventoryInSelectVM>();

            SimpleIoc.Default.Register<MaterialInventoryOutVM>();
            SimpleIoc.Default.Register<MaterialInventoryOutEditVM>();

            SimpleIoc.Default.Register<RecordTestVM>();
            SimpleIoc.Default.Register<RecordTestEditVM>();
            SimpleIoc.Default.Register<RecordTestSelectVM>();
            SimpleIoc.Default.Register<RecordTestDocVM>();

            SimpleIoc.Default.Register<RecordBondingVM>();
            SimpleIoc.Default.Register<RecordBondingEditVM>();
            SimpleIoc.Default.Register<RecordBondingSelectVM>();

            SimpleIoc.Default.Register<ProductVM>();
            SimpleIoc.Default.Register<ProductEditVM>();
            SimpleIoc.Default.Register<ProductSelectVM>();

            SimpleIoc.Default.Register<PlateVM>();
            SimpleIoc.Default.Register<PlateEditVM>();
            SimpleIoc.Default.Register<PlateSelectVM>();

            SimpleIoc.Default.Register<DeliveryVM>();
            SimpleIoc.Default.Register<DeliveryEditVM>();
            SimpleIoc.Default.Register<DeliveryItemEditVM>();
            SimpleIoc.Default.Register<DeliveryItemListVM>();

            SimpleIoc.Default.Register<RecordVHPVM>();
            SimpleIoc.Default.Register<RecordVHPQuickEditVM>();

            SimpleIoc.Default.Register<RecordMillingVM>();
            SimpleIoc.Default.Register<RecordMillingEditVM>();

            SimpleIoc.Default.Register<RecordDeMoldVM>();
            SimpleIoc.Default.Register<RecordDeMoldSelectVM>();
            SimpleIoc.Default.Register<RecordDeMoldEditVM>();

            SimpleIoc.Default.Register<RecordMachineVM>();
            SimpleIoc.Default.Register<RecordMachineEditVM>();
            SimpleIoc.Default.Register<RecordMachineSelectVM>();

            SimpleIoc.Default.Register<CustomerVM>();
            SimpleIoc.Default.Register<CustomerEditVM>();
        }
        #region Properties
        public NavigationVM Navigation
        {
            get { return SimpleIoc.Default.GetInstance<NavigationVM>(); }
        }
        public OrderVM Order
        {
            get { return SimpleIoc.Default.GetInstance<OrderVM>(); }
        }
        public OrderEditVM OrderEdit
        {
            get { return SimpleIoc.Default.GetInstance<OrderEditVM>(); }
        }
        public OrderCheckVM OrderCheck
        {
            get { return SimpleIoc.Default.GetInstance<OrderCheckVM>(); }
        }
        public OrderCheckEditVM OrderCheckEdit
        {
            get { return SimpleIoc.Default.GetInstance<OrderCheckEditVM>(); }
        }

        public MissonVM Misson
        {
            get { return SimpleIoc.Default.GetInstance<MissonVM>(); }
        }
        public MissonSelectVM MissonSelect
        {
            get { return SimpleIoc.Default.GetInstance<MissonSelectVM>(); }
        }
        public PlanVM Plan
        {
            get { return SimpleIoc.Default.GetInstance<PlanVM>(); }
        }
        public PlanSearchVM PlanSearch
        {
            get { return SimpleIoc.Default.GetInstance<PlanSearchVM>(); }
        }
        public PlanEditVM PlanEdit
        {
            get { return SimpleIoc.Default.GetInstance<PlanEditVM>(); }
        }
        public PlanSelectVM PlanSelect
        {
            get { return SimpleIoc.Default.GetInstance<PlanSelectVM>(); }
        }

        public MaterialNeedVM MaterialNeed
        {
            get { return SimpleIoc.Default.GetInstance<MaterialNeedVM>(); }
        }
        public MaterialNeedEditVM MaterialNeedEdit
        {
            get { return SimpleIoc.Default.GetInstance<MaterialNeedEditVM>(); }
        }
        public MaterialNeedSelectVM MaterialNeedSelect
        {
            get { return SimpleIoc.Default.GetInstance<MaterialNeedSelectVM>(); }
        }
        public MaterialOrderVM MaterialOrder
        {
            get { return SimpleIoc.Default.GetInstance<MaterialOrderVM>(); }
        }
        public MaterialOrderEditVM MaterialOrderEdit
        {
            get { return SimpleIoc.Default.GetInstance<MaterialOrderEditVM>(); }
        }
        public MaterialOrderItemEditVM MaterialOrderItemEdit
        {
            get { return SimpleIoc.Default.GetInstance<MaterialOrderItemEditVM>(); }
        }
        public MaterialOrderItemSelectVM MaterialOrderItemSelect
        {
            get { return SimpleIoc.Default.GetInstance<MaterialOrderItemSelectVM>(); }
        }
        public MaterialInventoryInVM MaterialInventoryIn
        {
            get { return SimpleIoc.Default.GetInstance<MaterialInventoryInVM>(); }
        }
        public MaterialInventoryInEditVM MaterialInventoryInEdit
        {
            get { return SimpleIoc.Default.GetInstance<MaterialInventoryInEditVM>(); }
        }
        public MaterialInventoryInSelectVM MaterialInventoryInSelect
        {
            get { return SimpleIoc.Default.GetInstance<MaterialInventoryInSelectVM>(); }
        }

        public MaterialInventoryOutVM MaterialInventoryOut
        {
            get { return SimpleIoc.Default.GetInstance<MaterialInventoryOutVM>(); }
        }
        public MaterialInventoryOutEditVM MaterialInventoryOutEdit
        {
            get { return SimpleIoc.Default.GetInstance<MaterialInventoryOutEditVM>(); }
        }



        public RecordVHPVM RecordVHP
        {
            get { return SimpleIoc.Default.GetInstance<RecordVHPVM>(); }
        }
        public RecordVHPQuickEditVM RecordVHPQuickEdit
        {
            get { return SimpleIoc.Default.GetInstance<RecordVHPQuickEditVM>(); }
        }

        public RecordTestVM RecordTest
        {
            get { return SimpleIoc.Default.GetInstance<RecordTestVM>(); }
        }
        public RecordTestEditVM RecordTestEdit
        {
            get { return SimpleIoc.Default.GetInstance<RecordTestEditVM>(); }
        }
        public RecordTestSelectVM RecordTestSelect
        {
            get { return SimpleIoc.Default.GetInstance<RecordTestSelectVM>(); }
        }
        public RecordTestDocVM RecordTestDoc
        {
            get { return SimpleIoc.Default.GetInstance<RecordTestDocVM>(); }
        }



        public RecordMillingVM RecordMilling
        {
            get
            {
                return SimpleIoc.Default.GetInstance<RecordMillingVM>();
            }
        }
        public RecordMillingEditVM RecordMillingEdit
        {
            get
            {
                return SimpleIoc.Default.GetInstance<RecordMillingEditVM>();
            }
        }


        public RecordDeMoldVM RecordDeMold
        {
            get
            {
                return SimpleIoc.Default.GetInstance<RecordDeMoldVM>();
            }
        }
        public RecordDeMoldSelectVM RecordDeMoldSelect
        {
            get
            {
                return SimpleIoc.Default.GetInstance<RecordDeMoldSelectVM>();
            }
        }
        public RecordDeMoldEditVM RecordDeMoldEdit
        {
            get
            {
                return SimpleIoc.Default.GetInstance<RecordDeMoldEditVM>();
            }
        }


        public RecordMachineVM RecordMachine
        {
            get
            {
                return SimpleIoc.Default.GetInstance<RecordMachineVM>();
            }
        }
        public RecordMachineEditVM RecordMachineEdit
        {
            get
            {
                return SimpleIoc.Default.GetInstance<RecordMachineEditVM>();
            }
        }

        public RecordMachineSelectVM RecordMachineSelect
        {
            get
            {
                return SimpleIoc.Default.GetInstance<RecordMachineSelectVM>();
            }
        }
        public RecordBondingVM RecordBonding
        {
            get
            {
                return SimpleIoc.Default.GetInstance<RecordBondingVM>();
            }
        }
        public RecordBondingEditVM RecordBondingEdit
        {
            get
            {
                return SimpleIoc.Default.GetInstance<RecordBondingEditVM>();
            }
        }
        public RecordBondingSelectVM RecordBondingSelect
        {
            get
            {
                return SimpleIoc.Default.GetInstance<RecordBondingSelectVM>();
            }
        }



        public ProductVM Product
        {
            get { return SimpleIoc.Default.GetInstance<ProductVM>(); }
        }
        public ProductEditVM ProductEdit
        {
            get { return SimpleIoc.Default.GetInstance<ProductEditVM>(); }
        }
        public ProductSelectVM ProductSelect
        {
            get { return SimpleIoc.Default.GetInstance<ProductSelectVM>(); }
        }

        public PlateVM Plate
        {
            get { return SimpleIoc.Default.GetInstance<PlateVM>(); }
        }
        public PlateEditVM PlateEdit
        {
            get { return SimpleIoc.Default.GetInstance<PlateEditVM>(); }
        }
        public PlateSelectVM PlateSelect
        {
            get { return SimpleIoc.Default.GetInstance<PlateSelectVM>(); }
        }


        public DeliveryVM Delivery
        {
            get { return SimpleIoc.Default.GetInstance<DeliveryVM>(); }
        }
        public DeliveryEditVM DeliveryEdit
        {
            get { return SimpleIoc.Default.GetInstance<DeliveryEditVM>(); }
        }
        public DeliveryItemEditVM DeliveryItemEdit
        {
            get { return SimpleIoc.Default.GetInstance<DeliveryItemEditVM>(); }
        }
        public DeliveryItemListVM DeliveryItemList
        {
            get { return SimpleIoc.Default.GetInstance<DeliveryItemListVM>(); }
        }

        public CustomerVM Customer
        {
            get { return SimpleIoc.Default.GetInstance<CustomerVM>(); }
        }
        public CustomerEditVM CustomerEdit
        {
            get { return SimpleIoc.Default.GetInstance<CustomerEditVM>(); }
        }
        #endregion


        public static void Cleanup()
        {
   
        }


    }
}