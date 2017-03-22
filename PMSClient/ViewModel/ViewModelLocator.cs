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
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
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
            SimpleIoc.Default.Register<OrderCheckVM>();
            SimpleIoc.Default.Register<MissonVM>();
            SimpleIoc.Default.Register<PlanVM>();

            SimpleIoc.Default.Register<MaterialNeedVM>();
            SimpleIoc.Default.Register<MaterialOrderVM>();
            SimpleIoc.Default.Register<OrderSelectMaterialNeedVM>();

            SimpleIoc.Default.Register<RecordVHPVM>();
            SimpleIoc.Default.Register<RecordTestVM>();

            SimpleIoc.Default.Register<RecordDeliveryVM>();
            SimpleIoc.Default.Register<RecordVHPQuickEditVM>();

            SimpleIoc.Default.Register<PlanSelectForRecordTestVM>();
            SimpleIoc.Default.Register<PlanSelectForRecordVHPVM>();

            SimpleIoc.Default.Register<RecordMillingVM>();
            SimpleIoc.Default.Register<RecordDeMoldVM>();
            SimpleIoc.Default.Register<RecordMachineVM>();




        }
        #region Properties
        public RecordVHPQuickEditVM RecordVHPQuickEdit
        {
            get { return SimpleIoc.Default.GetInstance<RecordVHPQuickEditVM>(); }
        }
        public RecordVHPVM RecordVHP
        {
            get { return SimpleIoc.Default.GetInstance<RecordVHPVM>(); }
        }
        public NavigationVM Navigation
        {
            get { return SimpleIoc.Default.GetInstance<NavigationVM>(); }
        }
        public OrderVM Order
        {
            get { return SimpleIoc.Default.GetInstance<OrderVM>(); }
        }
        public OrderCheckVM OrderCheck
        {
            get { return SimpleIoc.Default.GetInstance<OrderCheckVM>(); }
        }
        public MissonVM Misson
        {
            get { return SimpleIoc.Default.GetInstance<MissonVM>(); }
        }
        public PlanVM Plan
        {
            get { return SimpleIoc.Default.GetInstance<PlanVM>(); }
        }
        public MaterialNeedVM MaterialNeed
        {
            get { return SimpleIoc.Default.GetInstance<MaterialNeedVM>(); }
        }
        public MaterialOrderVM MaterialOrder
        {
            get { return SimpleIoc.Default.GetInstance<MaterialOrderVM>(); }
        }
        public OrderSelectMaterialNeedVM OrderSelectMaterialNeed
        {
            get { return SimpleIoc.Default.GetInstance<OrderSelectMaterialNeedVM>(); }
        }
        public RecordTestVM RecordTest
        {
            get { return SimpleIoc.Default.GetInstance<RecordTestVM>(); }
        }
        public RecordDeliveryVM RecordDelivery
        {
            get { return SimpleIoc.Default.GetInstance<RecordDeliveryVM>(); }
        }

        public PlanSelectForRecordTestVM PlanSelectForRecordTestResult
        {
            get { return SimpleIoc.Default.GetInstance<PlanSelectForRecordTestVM>(); }
        }
        public PlanSelectForRecordVHPVM PlanSelectForRecordVHP
        {
            get { return SimpleIoc.Default.GetInstance<PlanSelectForRecordVHPVM>(); }
        }

        public RecordMillingVM RecordMilling
        {
            get
            {
                return SimpleIoc.Default.GetInstance<RecordMillingVM>();
            }
        }

        public RecordDeMoldVM RecordDeMold
        {
            get
            {
                return SimpleIoc.Default.GetInstance<RecordDeMoldVM>();
            }
        }

        public RecordMachineVM RecordMachine
        {
            get
            {
                return SimpleIoc.Default.GetInstance<RecordMachineVM>();
            }
        }
        #endregion


        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }


    }
}