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

            SimpleIoc.Default.Register<OrderVM>(true);
            SimpleIoc.Default.Register<OrderEditVM>();
            SimpleIoc.Default.Register<OrderSelectVM>();

            SimpleIoc.Default.Register<OutSourceVM>();
            SimpleIoc.Default.Register<OutSourceEditVM>();
            SimpleIoc.Default.Register<OutSourceSelectVM>();


            SimpleIoc.Default.Register<MissonVM>(true);
            SimpleIoc.Default.Register<MissonSelectVM>();

            SimpleIoc.Default.Register<PlanVM>(true);
            SimpleIoc.Default.Register<PlanEditVM>();
            SimpleIoc.Default.Register<PlanSelectVM>();
            SimpleIoc.Default.Register<PlanConclusionVM>();
            SimpleIoc.Default.Register<PlanConclusionEditVM>();

            SimpleIoc.Default.Register<MaterialNeedVM>();
            SimpleIoc.Default.Register<MaterialNeedEditVM>();
            SimpleIoc.Default.Register<MaterialNeedSelectVM>();

            SimpleIoc.Default.Register<MaterialOrderVM>();
            SimpleIoc.Default.Register<MaterialOrderEditVM>();
            SimpleIoc.Default.Register<MaterialOrderItemEditVM>();
            SimpleIoc.Default.Register<MaterialOrderItemSelectVM>();
            SimpleIoc.Default.Register<MaterialOrderItemListVM>();
            SimpleIoc.Default.Register<MaterialOrderItemListUnCompletedVM>();


            SimpleIoc.Default.Register<MaterialInventoryInVM>(true);
            SimpleIoc.Default.Register<MaterialInventoryInEditVM>();
            SimpleIoc.Default.Register<MaterialInventoryInSelectVM>();
            SimpleIoc.Default.Register<MaterialInventoryInUnCompletedVM>();

            SimpleIoc.Default.Register<MaterialInventoryOutVM>(true);
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
            SimpleIoc.Default.Register<ProductUnCompletedVM>();

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

            SimpleIoc.Default.Register<OrderStatisticVM>();
            SimpleIoc.Default.Register<PlanStatisticVM>();
            SimpleIoc.Default.Register<DeliveryStatisticVM>();
            SimpleIoc.Default.Register<ProductStatisticVM>();

            SimpleIoc.Default.Register<FeedBackVM>();
            SimpleIoc.Default.Register<FeedBackEditVM>();

            SimpleIoc.Default.Register<CheckListVM>();
            SimpleIoc.Default.Register<CheckListEditVM>();
            SimpleIoc.Default.Register<CheckListReadVM>();

            SimpleIoc.Default.Register<IntegretedSearchVM>();
            SimpleIoc.Default.Register<OutputVM>();

            SimpleIoc.Default.Register<DebugVM>();


            SimpleIoc.Default.Register<CompoundVM>();
            SimpleIoc.Default.Register<CompoundEditVM>();

            SimpleIoc.Default.Register<ToDoVM>();
            SimpleIoc.Default.Register<ToDoEditVM>();

            SimpleIoc.Default.Register<FillingToolVM>();
            SimpleIoc.Default.Register<FillingToolEditVM>();

            SimpleIoc.Default.Register<MillingToolVM>();
            SimpleIoc.Default.Register<MillingToolWindowVM>();
            SimpleIoc.Default.Register<MillingToolEditVM>();

            SimpleIoc.Default.Register<FailureVM>();
            SimpleIoc.Default.Register<FailureEditVM>();

            SimpleIoc.Default.Register<PlanTraceVM>();


            SimpleIoc.Default.Register<PMICounterVM>();
            SimpleIoc.Default.Register<PMICounterEditVM>();

            SimpleIoc.Default.Register<RemainInventoryVM>();
            SimpleIoc.Default.Register<RemainInventoryEditVM>();

            SimpleIoc.Default.Register<OutsideProcessVM>();
            SimpleIoc.Default.Register<OutsideProcessEditVM>();

            SimpleIoc.Default.Register<SampleVM>();
            SimpleIoc.Default.Register<SampleEditVM>();
            SimpleIoc.Default.Register<RawMaterialSheetVM>();
            SimpleIoc.Default.Register<RawMaterialSheetEditVM>();
        }
        #region Properties
        public RawMaterialSheetVM RawMaterialSheet
        {
            get { return SimpleIoc.Default.GetInstance<RawMaterialSheetVM>(); }
        }

        public RawMaterialSheetEditVM RawMaterialSheetEdit
        {
            get { return SimpleIoc.Default.GetInstance<RawMaterialSheetEditVM>(); }
        }
        public SampleVM Sample
        {
            get { return SimpleIoc.Default.GetInstance<SampleVM>(); }
        }
        public SampleEditVM SampleEdit
        {
            get { return SimpleIoc.Default.GetInstance<SampleEditVM>(); }
        }

        public OutsideProcessVM OutsideProcess
        {
            get { return SimpleIoc.Default.GetInstance<OutsideProcessVM>(); }
        }
        public OutsideProcessEditVM OutsideProcessEdit
        {
            get { return SimpleIoc.Default.GetInstance<OutsideProcessEditVM>(); }
        }
        public RemainInventoryEditVM RemainInventoryEdit
        {
            get { return SimpleIoc.Default.GetInstance<RemainInventoryEditVM>(); }
        }
        public RemainInventoryVM RemainInventory
        {
            get { return SimpleIoc.Default.GetInstance<RemainInventoryVM>(); }
        }

        public PMICounterEditVM PMICounterEdit
        {
            get { return SimpleIoc.Default.GetInstance<PMICounterEditVM>(); }
        }
        public PMICounterVM PMICounter
        {
            get { return SimpleIoc.Default.GetInstance<PMICounterVM>(); }
        }

        public PlanTraceVM PlanTrace
        {
            get
            {
                return SimpleIoc.Default.GetInstance<PlanTraceVM>();
            }
        }
        public FailureVM Failure
        {
            get { return SimpleIoc.Default.GetInstance<FailureVM>(); }
        }

        public FailureEditVM FailureEdit
        {
            get { return SimpleIoc.Default.GetInstance<FailureEditVM>(); }
        }

        public FillingToolVM FillingTool
        {
            get
            {
                return SimpleIoc.Default.GetInstance<FillingToolVM>();
            }
        }

        public FillingToolEditVM FillingToolEdit
        {
            get
            {
                return SimpleIoc.Default.GetInstance<FillingToolEditVM>();
            }
        }

        public MillingToolVM MillingTool
        {
            get
            {
                return SimpleIoc.Default.GetInstance<MillingToolVM>();
            }
        }

        public MillingToolWindowVM MillingToolWindow
        {
            get
            {
                return SimpleIoc.Default.GetInstance<MillingToolWindowVM>();
            }
        }

        public MillingToolEditVM MillingToolEdit
        {
            get
            {
                return SimpleIoc.Default.GetInstance<MillingToolEditVM>();
            }
        }

        public ToDoVM ToDo
        {
            get { return SimpleIoc.Default.GetInstance<ToDoVM>(); }
        }
        public ToDoEditVM ToDoEdit
        {
            get { return SimpleIoc.Default.GetInstance<ToDoEditVM>(); }
        }

        public CompoundVM Compound
        {
            get { return SimpleIoc.Default.GetInstance<CompoundVM>(); }
        }
        public CompoundEditVM CompoundEdit
        {
            get { return SimpleIoc.Default.GetInstance<CompoundEditVM>(); }
        }
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
        public OrderSelectVM OrderSelect
        {
            get { return SimpleIoc.Default.GetInstance<OrderSelectVM>(); }
        }
        public OutSourceVM OutSource
        {
            get { return SimpleIoc.Default.GetInstance<OutSourceVM>(); }
        }
        public OutSourceEditVM OutSourceEdit
        {
            get { return SimpleIoc.Default.GetInstance<OutSourceEditVM>(); }
        }
        public OutSourceSelectVM OutSourceSelect
        {
            get { return SimpleIoc.Default.GetInstance<OutSourceSelectVM>(); }
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

        public PlanEditVM PlanEdit
        {
            get { return SimpleIoc.Default.GetInstance<PlanEditVM>(); }
        }
        public PlanSelectVM PlanSelect
        {
            get { return SimpleIoc.Default.GetInstance<PlanSelectVM>(); }
        }
        public PlanConclusionVM PlanConclusion
        {
            get { return SimpleIoc.Default.GetInstance<PlanConclusionVM>(); }
        }
        public PlanConclusionEditVM PlanConclusionEdit
        {
            get { return SimpleIoc.Default.GetInstance<PlanConclusionEditVM>(); }
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
        public MaterialOrderItemListVM MaterialOrderItemList
        {
            get { return SimpleIoc.Default.GetInstance<MaterialOrderItemListVM>(); }
        }

        public MaterialOrderItemListUnCompletedVM MaterialOrderItemListUnCompleted
        {
            get { return SimpleIoc.Default.GetInstance<MaterialOrderItemListUnCompletedVM>(); }
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
        public MaterialInventoryInUnCompletedVM MaterialInventoryInUnCompleted
        {
            get { return SimpleIoc.Default.GetInstance<MaterialInventoryInUnCompletedVM>(); }
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
        public ProductUnCompletedVM ProductUnCompleted
        {
            get { return SimpleIoc.Default.GetInstance<ProductUnCompletedVM>(); }
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
        public OrderStatisticVM OrderStatistic
        {
            get { return SimpleIoc.Default.GetInstance<OrderStatisticVM>(); }
        }
        public PlanStatisticVM PlanStatistic
        {
            get { return SimpleIoc.Default.GetInstance<PlanStatisticVM>(); }
        }
        public ProductStatisticVM ProductStatistic
        {
            get { return SimpleIoc.Default.GetInstance<ProductStatisticVM>(); }
        }
        public DeliveryStatisticVM DeliveryStatistic
        {
            get { return SimpleIoc.Default.GetInstance<DeliveryStatisticVM>(); }
        }
        public FeedBackVM FeedBack
        {
            get { return SimpleIoc.Default.GetInstance<FeedBackVM>(); }
        }
        public FeedBackEditVM FeedBackEdit
        {
            get { return SimpleIoc.Default.GetInstance<FeedBackEditVM>(); }
        }
        public CheckListVM CheckList
        {
            get { return SimpleIoc.Default.GetInstance<CheckListVM>(); }
        }
        public CheckListEditVM CheckListEdit
        {
            get { return SimpleIoc.Default.GetInstance<CheckListEditVM>(); }
        }
        public CheckListReadVM CheckListRead
        {
            get { return SimpleIoc.Default.GetInstance<CheckListReadVM>(); }
        }
        public IntegretedSearchVM IntegretedSearch
        {
            get { return SimpleIoc.Default.GetInstance<IntegretedSearchVM>(); }
        }
        public OutputVM Output
        {
            get { return SimpleIoc.Default.GetInstance<OutputVM>(); }
        }
        public DebugVM Debug
        {
            get { return SimpleIoc.Default.GetInstance<DebugVM>(); }
        }
        #endregion


        public static void Cleanup()
        {

        }


    }
}