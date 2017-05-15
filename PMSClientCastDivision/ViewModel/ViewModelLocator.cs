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

            SimpleIoc.Default.Register<MaterialOrderVM>();
            SimpleIoc.Default.Register<MaterialOrderEditVM>();
            SimpleIoc.Default.Register<MaterialOrderItemEditVM>();
            SimpleIoc.Default.Register<MaterialOrderItemSelectVM>();
            SimpleIoc.Default.Register<MaterialOrderItemListVM>();

            SimpleIoc.Default.Register<MaterialInventoryInVM>();
            SimpleIoc.Default.Register<MaterialInventoryInEditVM>();
            SimpleIoc.Default.Register<MaterialInventoryInSelectVM>();

            SimpleIoc.Default.Register<MaterialInventoryOutVM>();
            SimpleIoc.Default.Register<MaterialInventoryOutEditVM>();

            SimpleIoc.Default.Register<OutputVM>();

            SimpleIoc.Default.Register<DebugVM>();
        }
        #region Properties
        public NavigationVM Navigation
        {
            get { return SimpleIoc.Default.GetInstance<NavigationVM>(); }
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