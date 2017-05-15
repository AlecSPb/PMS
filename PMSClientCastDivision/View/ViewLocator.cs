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
            SimpleIoc.Default.Register<MaterialOrderView>();
            SimpleIoc.Default.Register<MaterialOrderItemListView>();
            SimpleIoc.Default.Register<MaterialInventoryInView>();
            SimpleIoc.Default.Register<MaterialInventoryOutView>();
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

        public MaterialOrderView MaterialOrder
        {
            get { return SimpleIoc.Default.GetInstance<MaterialOrderView>(); }
        }
        public MaterialOrderItemListView MaterialOrderItemList
        {
            get { return SimpleIoc.Default.GetInstance<MaterialOrderItemListView>(); }
        }
        public MaterialInventoryInView MaterialInventoryIn
        {
            get { return SimpleIoc.Default.GetInstance<MaterialInventoryInView>(); }
        }
  
        public MaterialInventoryOutView MaterialInventoryOut
        {
            get { return SimpleIoc.Default.GetInstance<MaterialInventoryOutView>(); }
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
