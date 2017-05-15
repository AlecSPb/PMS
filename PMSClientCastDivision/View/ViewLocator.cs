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

            SimpleIoc.Default.Register<MaterialOrderView>();
            SimpleIoc.Default.Register<MaterialOrderEditView>();
            SimpleIoc.Default.Register<MaterialOrderItemEditView>();
            SimpleIoc.Default.Register<MaterialOrderItemSelectView>();
            SimpleIoc.Default.Register<MaterialOrderItemListView>();


            SimpleIoc.Default.Register<MaterialInventoryInView>();
            SimpleIoc.Default.Register<MaterialInventoryInEditView>();
            SimpleIoc.Default.Register<MaterialInventoryInSelectView>();
            SimpleIoc.Default.Register<MaterialInventoryOutView>();
            SimpleIoc.Default.Register<MaterialInventoryOutEditView>();
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
