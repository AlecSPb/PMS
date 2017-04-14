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

            //SimpleIoc.Default.Register<RecordDeliveryView>();
            //SimpleIoc.Default.Register<RecordDeliveryEditView>();
            //SimpleIoc.Default.Register<RecordDeliveryItemEditView>();

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

        //public RecordDeliveryView RecordDelivery
        //{
        //    get { return SimpleIoc.Default.GetInstance<RecordDeliveryView>(); }
        //}
        //public RecordDeliveryEditView RecordDeliveryEdit
        //{
        //    get { return SimpleIoc.Default.GetInstance<RecordDeliveryEditView>(); }
        //}
        //public RecordDeliveryItemEditView RecordDeliveryItemEdit
        //{
        //    get { return SimpleIoc.Default.GetInstance<RecordDeliveryItemEditView>(); }
        //}

        #endregion
    }
}
