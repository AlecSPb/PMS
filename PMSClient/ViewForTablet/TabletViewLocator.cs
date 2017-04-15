using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using PMSClient.ViewForTablet;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Ioc;

namespace PMSClient.ViewForTablet
{
    public class TabletViewLocator
    {
        public TabletViewLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            //SimpleIoc.Default.Register<NavigationView>(true);
            //SimpleIoc.Default.Register<RecordVHPView>();
            //SimpleIoc.Default.Register<RecordVHPQuickEditView>();

            //SimpleIoc.Default.Register<PlanSelectView>();
            //SimpleIoc.Default.Register<RecordTestView>();
            //SimpleIoc.Default.Register<RecordTestSelectView>();
            //SimpleIoc.Default.Register<RecordTestEditView>();

            //SimpleIoc.Default.Register<DeliveryView>();
            //SimpleIoc.Default.Register<DeliveryEditView>();
            //SimpleIoc.Default.Register<DeliveryItemEditView>();

        }
        #region ViewProperties
        //public NavigationView Navigation
        //{
        //    get
        //    {
        //        return SimpleIoc.Default.GetInstance<NavigationView>();
        //    }
        //}
        //public RecordVHPView RecordVHP
        //{
        //    get { return SimpleIoc.Default.GetInstance<RecordVHPView>(); }
        //}
        //public RecordVHPQuickEditView RecordVHPQuickEdit
        //{
        //    get { return SimpleIoc.Default.GetInstance<RecordVHPQuickEditView>(); }
        //}

        //public PlanSelectView PlanSelect
        //{
        //    get { return SimpleIoc.Default.GetInstance<PlanSelectView>(); }
        //}

        //public RecordTestView RecordTest
        //{
        //    get
        //    {
        //        return SimpleIoc.Default.GetInstance<RecordTestView>();
        //    }
        //}
        //public RecordTestEditView RecordTestEdit
        //{
        //    get
        //    {
        //        return SimpleIoc.Default.GetInstance<RecordTestEditView>();
        //    }
        //}
        //public RecordTestSelectView RecordTestSelect
        //{
        //    get
        //    {
        //        return SimpleIoc.Default.GetInstance<RecordTestSelectView>();
        //    }
        //}

        //public DeliveryView Delivery
        //{
        //    get { return SimpleIoc.Default.GetInstance<DeliveryView>(); }
        //}
        //public DeliveryEditView DeliveryEdit
        //{
        //    get { return SimpleIoc.Default.GetInstance<DeliveryEditView>(); }
        //}
        //public DeliveryItemEditView DeliveryItemEdit
        //{
        //    get { return SimpleIoc.Default.GetInstance<DeliveryItemEditView>(); }
        //}

        #endregion
    }
}
