/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:PMSDesktopClient"
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

namespace PMSTabletClient.ViewModel
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

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}
            SimpleIoc.Default.Register<NavigationVM>(true);
            //SimpleIoc.Default.Register<OrderVM>();
            //SimpleIoc.Default.Register<OrderCheckVM>();
            //SimpleIoc.Default.Register<MissonVM>();
            //SimpleIoc.Default.Register<PlanVM>();

            //SimpleIoc.Default.Register<MaterialNeedVM>();
            //SimpleIoc.Default.Register<MaterialOrderVM>();
            //SimpleIoc.Default.Register<OrderSelectMaterialNeedVM>();

            SimpleIoc.Default.Register<RecordVHPVM>();
            SimpleIoc.Default.Register<RecordVHPQuickEditVM>();

            //SimpleIoc.Default.Register<RecordTestResultVM>();
            //SimpleIoc.Default.Register<RecordDeliveryVM>();
            //SimpleIoc.Default.Register<PlanSelectForRecordTestResultVM>();
            //SimpleIoc.Default.Register<PlanSelectForRecordVHPVM>();








        }
        #region Properties
        public NavigationVM Navigation
        {
            get { return SimpleIoc.Default.GetInstance<NavigationVM>(); }
        }
        public RecordVHPVM RecordVHP
        {
            get { return SimpleIoc.Default.GetInstance<RecordVHPVM>(); }
        }
        public RecordVHPQuickEditVM RecordVHPQuickEdit
        {
            get { return SimpleIoc.Default.GetInstance<RecordVHPQuickEditVM>(); }
        }
        //public OrderVM Order
        //{
        //    get { return SimpleIoc.Default.GetInstance<OrderVM>(); }
        //}
        //public OrderCheckVM OrderCheck
        //{
        //    get { return SimpleIoc.Default.GetInstance<OrderCheckVM>(); }
        //}
        //public MissonVM Misson
        //{
        //    get { return SimpleIoc.Default.GetInstance<MissonVM>(); }
        //}
        //public PlanVM Plan
        //{
        //    get { return SimpleIoc.Default.GetInstance<PlanVM>(); }
        //}
        //public MaterialNeedVM MaterialNeed
        //{
        //    get { return SimpleIoc.Default.GetInstance<MaterialNeedVM>(); }
        //}
        //public MaterialOrderVM MaterialOrder
        //{
        //    get { return SimpleIoc.Default.GetInstance<MaterialOrderVM>(); }
        //}
        //public OrderSelectMaterialNeedVM OrderSelectMaterialNeed
        //{
        //    get { return SimpleIoc.Default.GetInstance<OrderSelectMaterialNeedVM>(); }
        //}
        //public RecordTestResultVM RecordTestResult
        //{
        //    get { return SimpleIoc.Default.GetInstance<RecordTestResultVM>(); }
        //}
        //public RecordDeliveryVM RecordDelivery
        //{
        //    get { return SimpleIoc.Default.GetInstance<RecordDeliveryVM>(); }
        //}

        //public PlanSelectForRecordTestResultVM PlanSelectForRecordTestResult
        //{
        //    get { return SimpleIoc.Default.GetInstance<PlanSelectForRecordTestResultVM>(); }
        //}

        //public PlanSelectForRecordVHPVM PlanSelectForRecordVHP
        //{
        //    get { return SimpleIoc.Default.GetInstance<PlanSelectForRecordVHPVM>(); }
        //}

        #endregion


        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }


    }
}