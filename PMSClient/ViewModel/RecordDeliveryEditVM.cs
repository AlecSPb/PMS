using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;

namespace PMSClient.ViewModel
{
    public class RecordDeliveryEditVM : BaseViewModelEdit
    {
        public RecordDeliveryEditVM()
        {
            InitialCommands();
            InitialProperties();
        }

        public void SetNew()
        {
            IsNew = true;
            #region 初始化
            var model = new DcRecordDelivery();
            model.ID = Guid.NewGuid();
            model.InvoiceNumber = "InvoiceNumber";
            model.DeliveryName = DateTime.Now.ToString("yyMMdd") + "A";
            model.DeliveryNumber = "UPS";
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.State = PMSCommon.CommonState.UnChecked.ToString();
            model.PackageInformation = "50kg";
            model.PackageType = "Wood";
            model.Remark = "";
            model.ShipTime = DateTime.Now;
            model.Address = "Address Here";
            model.Country = "USA";
            model.State = PMSCommon.SimpleState.UnDeleted.ToString();
            #endregion
            CurrentRecordDelivery = model;
        }
        public void SetEdit(DcRecordDelivery model)
        {
            if (model!=null)
            {
                IsNew = false;
                CurrentRecordDelivery = model;
            }
        }




        private void InitialProperties()
        {
            OrderStates = new ObservableCollection<string>();
            var states = Enum.GetNames(typeof(PMSCommon.TestResultState));
            states.ToList().ForEach(s => OrderStates.Add(s));

            Countries = new ObservableCollection<string>();
            var countries = Enum.GetNames(typeof(PMSCommon.Country));
            countries.ToList().ForEach(s => Countries.Add(s));
        }

        private void InitialCommands()
        {
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
        }

        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.RecordDelivery);
        }

        private void ActionSave()
        {
            try
            {
                var service = new RecordDeliveryServiceClient();
                if (IsNew)
                {
                    service.AddRecordDelivery(CurrentRecordDelivery);
                }
                else
                {
                    service.UpdateReocrdDelivery(CurrentRecordDelivery);
                }
                PMSHelper.ViewModels.RecordDelivery.RefreshData();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }

        }


        #region Properties
        public DcRecordDelivery CurrentRecordDelivery { get; set; }
        public ObservableCollection<string> OrderStates { get; set; }
        public ObservableCollection<string> Countries { get; set; }
        #endregion

    }
}
